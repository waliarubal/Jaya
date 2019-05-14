import { OnDestroy } from '@angular/core';
import { BaseService } from '@common/index';

export abstract class SuperService extends BaseService implements OnDestroy {
    
    ngOnDestroy(): void {
        this.Dispose();
    }

    protected abstract Dispose(): void;

}