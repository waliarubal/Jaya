import { BaseComponent } from '@shared/base.component';
import { ConfigModel } from '@common/index';
import { ConfigService } from '@services/config.service';
import { Component } from '@angular/core';

@Component({
    selector: 'fs-detail',
    templateUrl: './fs-detail.component.html'
})
export class FileSystemDetailComponent extends BaseComponent {
    
    constructor(config: ConfigService){
        super(config);
    }

    protected OnConfigChanged(config: ConfigModel): void {
        
    }

    protected async Initialize(): Promise<void> {
        
    }

    protected async Destroy(): Promise<void> {
        
    }

}