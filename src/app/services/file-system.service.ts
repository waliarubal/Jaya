import { Injectable } from '@angular/core';
import { IpcBaseService } from '@shared/ipc-base.service';
import { MessageType, DirectoryModel, Helpers, ProviderModel } from '@common/index';

@Injectable()
export class FileSystemService extends IpcBaseService {

    constructor() {
        super();
    }

    protected Dispose(): void {
        this.Receive.unsubscribe();
    }

    async GetDirectories(path: string): Promise<DirectoryModel> {
        let response = await this.SendAsync(MessageType.Directoties, path);
        let directory = Helpers.Deserialize<DirectoryModel>(response.DataJson);
        return directory;
    }

    async GetProvider(): Promise<ProviderModel> {
        let response = await this.SendAsync(MessageType.FileSystemProvider);
        let provider = Helpers.Deserialize<ProviderModel>(response.DataJson);
        console.log(provider instanceof ProviderModel);
        return provider;
    }

}