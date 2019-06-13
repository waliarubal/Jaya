import { Injectable } from '@angular/core';
import { IpcService } from '@services/ipc.service';
import { MessageType, DirectoryModel, Helpers, ProviderModel, IProviderService, ProviderType } from '@common/index';
import { SuperService } from '@shared/super.service';

@Injectable()
export class FileSystemService extends SuperService implements IProviderService {

    constructor(private readonly _ipc: IpcService) {
        super();
    }

    get Type(): ProviderType {
        return ProviderType.FileSystem;
    }

    protected Dispose(): void {
        this._ipc.Receive.unsubscribe();
    }

    async GetDirectory(path: string): Promise<DirectoryModel> {
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