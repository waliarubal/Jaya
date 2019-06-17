import { Dropbox } from 'dropbox';
import * as fetch from 'isomorphic-fetch';
import { Constants, MessageModel, MessageType, ProviderModel, Helpers, IProviderService, DirectoryModel, ProviderType, FileModel } from '../../../src-common';
import { IpcService } from '..';
import { SuperService, ElectronHelpers } from '../../shared';

export class DropboxService extends SuperService implements IProviderService {
    private readonly APP_KEY = 'wr1084dwe5oimdh';
    private readonly REDIRECT_URL = 'http://localhost:7860/login/callback';

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
                const authUrl = this._client.getAuthenticationUrl(this.REDIRECT_URL, 'dropbox-auth', 'token');
                ElectronHelpers.OpenModal(authUrl, this.REDIRECT_URL, this._ipc.Window).then(window => {
                    if (window.webContents) {
                        window.webContents.on('did-finish-load', () => {
                            let history = ElectronHelpers.GetHistory(window);
                            if (history && history.length > 0 && history[history.length - 1].startsWith(this.REDIRECT_URL)) {
                                let authUrl = history[history.length - 1];
                                
                                message.DataJson = authUrl;
                                this._ipc.Send(message);
                            }
                        });
                    } else {
                        message.DataJson = null;
                        this._ipc.Send(message);
                    }

                });
                break;
        }
    }
}