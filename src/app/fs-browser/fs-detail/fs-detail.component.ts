import { BaseComponent } from '@shared/base.component';
import { ConfigModel, IFileSystemObject } from '@common/index';
import { ConfigService } from '@services/config.service';
import { Component, Input } from '@angular/core';

@Component({
    selector: 'fs-detail',
    templateUrl: './fs-detail.component.html'
})
export class FileSystemDetailComponent extends BaseComponent {
    private _sleectedObjects: IFileSystemObject[];

    constructor(config: ConfigService){
        super(config);
    }

    @Input()
    get SelectedObjects(): IFileSystemObject[] {
        return this._sleectedObjects;
    }

    set SelectedObjects(value: IFileSystemObject[]) {
        this._sleectedObjects = value;
    }

    protected OnConfigChanged(config: ConfigModel): void {
        
    }

    protected async Initialize(): Promise<void> {
        
    }

    protected async Destroy(): Promise<void> {
        
    }

}