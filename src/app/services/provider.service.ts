import { SuperService } from '@shared/super.service';
import { IpcService } from '@services/ipc.service';
import { Injectable } from '@angular/core';
import { Dictionary, ProviderType, IProviderService, ProviderModel } from '@common/index';
import { FileSystemService } from '@services/providers/file-system.service';
import { DropboxService } from '@services/providers/dropbox.service';

@Injectable()
export class ProviderService extends SuperService {
    private readonly _providers: Dictionary<ProviderType, IProviderService>;

    constructor(private readonly _ipc: IpcService,
        s1: FileSystemService,
        s2: DropboxService) {
            
        super();
        this._providers = new Dictionary();
        this._providers.Set(s1.Type, s1);
        this._providers.Set(s2.Type, s2);
    }

    protected Dispose(): void {
        this._ipc.Receive.unsubscribe();
    }

    GetService(type: ProviderType): IProviderService {
        return this._providers.Get(type);
    }

    async GetProviders(): Promise<ProviderModel[]> {
        try {
            let providers: ProviderModel[] = [];

            for (let service of this._providers.Values) {
                const provider = await service.GetProvider();
                providers.push(provider);
            }

            return providers;
        } catch (ex) {
            console.log(ex);
        }
    }

}