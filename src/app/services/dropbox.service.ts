import { Injectable } from '@angular/core';
import { IpcService } from '@services/ipc.service';
import { ProviderModel, MessageType, Helpers } from '@common/index';
import { SuperService } from '@shared/super.service';

@Injectable()
export class DropboxService extends SuperService {

    constructor(private readonly _ipc: IpcService) {
        super();
    }

    protected Dispose(): void {
        this._ipc.Receive.unsubscribe();
    }

    async GetProvider(): Promise<ProviderModel> {
        let response = await this._ipc.SendAsync(MessageType.DropboxProvider);
        let provider = Helpers.Deserialize<ProviderModel>(response.DataJson, ProviderModel);
        return provider;
    }
}