import { ConfigService } from '@services/config.service';
import { ConfigModel } from '@common/index';
import { BaseComponent } from '@shared/base.component';
import { Component } from '@angular/core';

@Component({
    selector: 'fs-preview',
    templateUrl: './fs-preview.component.html'
})
export class FileSystemPreviewComponent extends BaseComponent {
    
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