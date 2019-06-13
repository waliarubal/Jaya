import { SuperService } from '@shared/super.service';
import { IpcService } from '@services/ipc.service';
import { Injectable } from '@angular/core';
import { Dictionary, ProviderType, IProviderService } from '@common/index';
import { FileSystemService } from './providers/file-system.service';
import { DropboxService } from './providers/dropbox.service';

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

}