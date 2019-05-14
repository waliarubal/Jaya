import { Injectable } from '@angular/core';
import { IpcService } from '@services/ipc.service';
import { MessageType, DirectoryModel, Helpers, ProviderModel, BaseService } from '@common/index';

@Injectable()
export class FileSystemService extends BaseService {

    constructor(private readonly _ipc: IpcService) {
        super();
    }

    protected Dispose(): void {
        this._ipc.Receive.unsubscribe();
    }

    async GetDirectories(path: string): Promise<DirectoryModel> {
        let response = await this._ipc.SendAsync(MessageType.Directoties, path);
        let directory = Helpers.Deserialize<DirectoryModel>(response.DataJson, DirectoryModel);
        return directory;
    }

    async GetProvider(): Promise<ProviderModel> {
        let response = await this._ipc.SendAsync(MessageType.FileSystemProvider);
        let provider = Helpers.Deserialize<ProviderModel>(response.DataJson, ProviderModel);
        return provider;
    }

}