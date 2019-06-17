import { Dropbox } from 'dropbox';
import * as fetch from 'isomorphic-fetch';
import { Constants, MessageModel, MessageType, ProviderModel, Helpers, IProviderService, DirectoryModel, ProviderType, FileModel, AccountModel } from '../../../src-common';
import { IpcService } from '../ipc.service';
import { SuperService, ElectronHelpers } from '../../shared';
import * as Url from 'url';

export class DropboxService extends SuperService implements IProviderService {
    private readonly APP_KEY = 'wr1084dwe5oimdh';
    private readonly REDIRECT_URL = 'https://github.com/waliarubal/Jaya';

    private readonly _client: Dropbox;

    constructor(private readonly _ipc: IpcService) {
        super();
        this._ipc.Receive.on(Constants.IPC_CHANNEL, (message: MessageModel) => this.OnMessage(message));
        this._client = new Dropbox({ clientId: this.APP_KEY, fetch: fetch });
    }

    get Type(): ProviderType {
        return ProviderType.Dropbox;
    }

    get IsRootDrive(): boolean {
        return false;
    }

    protected async Dispose(): Promise<void> {
        this._ipc.Receive.removeAllListeners(Constants.IPC_CHANNEL);
    }

    async GetProvider(): Promise<ProviderModel> {
        let provider = ProviderModel.New(ProviderType.Dropbox, `Dropbox`, 'fab fa-dropbox');
        return new Promise<ProviderModel>((resolve, reject) => {
            if (provider)
                resolve(provider);
            else
                reject('Failed to get Dropbox provider.');
        });
    }

    async GetDirectory(path: string): Promise<DirectoryModel> {
        let directory = new DirectoryModel();
        directory.Path = path;
        directory.Directories = [];
        directory.Files = [];

        let result = await this._client.filesListFolder({ path: path });
        let isHavingData: boolean;
        do {
            for (let entry of result.entries) {
                switch (entry[".tag"]) {
                    case "file":
                        let file = new FileModel();
                        file.Name = entry.name;
                        file.Path = entry.path_lower;
                        file.Size = entry.size;
                        directory.Files.push(file);
                        break;

                    case "folder":
                        let dir = new DirectoryModel();
                        dir.Name = entry.name;
                        dir.Path = entry.path_lower;
                        directory.Directories.push(dir);
                        break;
                }
            }

            isHavingData = result.has_more;

            result = await this._client.filesListFolderContinue({ cursor: result.cursor });
        } while (isHavingData);

        return directory;
    }

    private async Authenticate(): Promise<AccountModel> {
        const authUrl = this._client.getAuthenticationUrl(this.REDIRECT_URL, 'dropbox-auth', 'token');
        const window = await ElectronHelpers.OpenModal(authUrl, this._ipc.Window);
        const page = window.webContents;
        if (page) {
            page.on('did-navigate', async (event: Electron.Event, url: string, code: number, status: string) => {
                if (url && url.startsWith(this.REDIRECT_URL)) {
                    let hash = Helpers.ParseUrlHash(url);
                    if (hash) {
                        if (hash.IsHaving('error')) {
                            window.close();
                            return null;
                        }
                        else if (hash.IsHaving('access_token')) {
                            let user = await this._client.usersGetAccount({ account_id: hash.Get('uid') });
                            let account = new AccountModel(user.email, ProviderType.Dropbox, hash.Get('access_token'));
                            window.close();
                            return account;
                        } 
                    }
                }
            });
            page.on('did-fail-load', (event: Electron.Event, code: number, desc: string, url: string, isMainFrame: boolean, procId: number, routingId: number) => {
                console.log(url);
                console.log(code);
                // if (code < 0) {
                //     window.close();
                //     return null;
                // } 
            });
        } else
            return null;
    }

    private OnMessage(message: MessageModel): void {
        switch (message.Type) {
            case MessageType.DropboxProvider:
                this.GetProvider().then(provider => {
                    message.DataJson = Helpers.Serialize<ProviderModel>(provider);
                    this._ipc.Send(message);
                }).catch(ex =>
                    console.log(ex)
                );
                break;

            case MessageType.DropboxDirectories:
                this.GetDirectory(message.DataJson).then(directory => {
                    message.DataJson = Helpers.Serialize<DirectoryModel>(directory);
                    this._ipc.Send(message);
                }).catch(ex =>
                    console.log(ex)
                );
                break;

            case MessageType.DropboxAuthenticate:
                this.Authenticate().then((account: AccountModel) => {
                    message.DataJson = Helpers.Serialize(account);
                    this._ipc.Send(message);
                }).catch(ex =>
                    console.log(ex)
                );
                break;
        }
    }
}