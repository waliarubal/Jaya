import { Injectable } from '@angular/core';
import { IpcService } from '@services/ipc.service';
import { ProviderModel, MessageType, Helpers, IProviderService, DirectoryModel, ProviderType, AccountModel } from '@common/index';
import { SuperService } from '@shared/super.service';

@Injectable()
export class DropboxService extends SuperService implements IProviderService {

    constructor(private readonly _ipc: IpcService) {
        super();
    }

    get Type(): ProviderType {
        return ProviderType.Dropbox;
    }

    get IsRootDrive(): boolean {
        return false;
    }

    protected Dispose(): void {
        this._ipc.Receive.unsubscribe();
    }

    async Authenticate(): Promise<AccountModel> {
        let response = await this._ipc.SendAsync(MessageType.DropboxAuthenticate);
        if (response.DataJson) {
            let account = Helpers.Deserialize<AccountModel>(response.DataJson, AccountModel);
            return account;
        }

        return null;
    }

    async GetProvider(): Promise<ProviderModel> {
        let response = await this._ipc.SendAsync(MessageType.DropboxProvider);
        let provider = Helpers.Deserialize<ProviderModel>(response.DataJson, ProviderModel);
        return provider;
    }

    async GetDirectory(path: string): Promise<DirectoryModel> {
        let response = await this._ipc.SendAsync(MessageType.DropboxDirectories, path);
        let directory = Helpers.Deserialize<DirectoryModel>(response.DataJson, DirectoryModel);
        return directory;
    }
}