import { Injectable } from '@angular/core';
import { IpcBaseService } from '@shared/ipc-base.service';
import { ProviderModel, MessageType, Helpers } from '@common/index';

@Injectable()
export class DropboxService extends IpcBaseService {
    constructor(){
        super();
    }

    protected Dispose(): void {
        this.Receive.unsubscribe();
    }

    async GetProvider(): Promise<ProviderModel> {
        let response = await this.SendAsync(MessageType.DropboxProvider);
        let provider = Helpers.Deserialize<ProviderModel>(response.DataJson, ProviderModel);
        return provider;
    }
}