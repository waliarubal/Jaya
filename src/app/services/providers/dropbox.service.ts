import { Injectable } from '@angular/core';
import { IpcService } from '@services/ipc.service';
import { ProviderModel, MessageType, Helpers, IProviderService, DirectoryModel, ProviderType } from '@common/index';
import { SuperService } from '@shared/super.service';

@Injectable()
export class DropboxService extends SuperService implements IProviderService {

    constructor(private readonly _ipc: IpcService) {
        super();
    }

    get Type(): ProviderType {
        return ProviderType.Dropbox;
    }

    protected Dispose(): void {
        this._ipc.Receive.unsubscribe();
    }

    async GetProvider(): Promise<ProviderModel> {
        let response = await this._ipc.SendAsync(MessageType.DropboxProvider);
        let provider = Helpers.Deserialize<ProviderModel>(response.DataJson, ProviderModel);
        return provider;
    }

    async GetDirectory(path: string): Promise<DirectoryModel> {
        let response = await this._ipc.SendAsync(MessageType.Directoties, path);
        let directory = Helpers.Deserialize<DirectoryModel>(response.DataJson, DirectoryModel);
        return directory;
    }
}