import { Constants, MessageModel, MessageType, ProviderModel, Helpers, IProviderService, DirectoryModel, ProviderType, FileModel } from '../../../src-common';
import { IpcService } from '..';
import { SuperService } from '../../shared';
import { Dropbox } from 'dropbox';

export class DropboxService extends SuperService implements IProviderService {
    private readonly ACCESS_TOKEN = 'JYfV_JpVuKMAAAAAAAADeXBqB1cjfNYgLPeyWRkhCgmj9OIjnRGpyKlgbb87mx4p';
    private readonly _client: Dropbox;

    constructor(private readonly _ipc: IpcService) {
        super();
        this._ipc.Receive.on(Constants.IPC_CHANNEL, (message: MessageModel) => this.OnMessage(message));
        this._client = new Dropbox({ accessToken: this.ACCESS_TOKEN, fetch: () => { } });
    }

    get Type(): ProviderType {
        return ProviderType.Dropbox;
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
        }
    }
}