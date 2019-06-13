import { ConfigService } from '@services/config.service';
import { ConfigModel, IFileSystemObject } from '@common/index';
import { BaseComponent } from '@shared/base.component';
import { Component, Input } from '@angular/core';

@Component({
    selector: 'fs-preview',
    templateUrl: './fs-preview.component.html'
})
export class FileSystemPreviewComponent extends BaseComponent {
    private _selectedObject: IFileSystemObject;

    constructor(config: ConfigService){
        super(config);
    }

    @Input()
    get SelectedObject(): IFileSystemObject {
        return this._selectedObject;
    }

    set SelectedObject(value: IFileSystemObject) {
        this._selectedObject = value;
    }

    protected OnConfigChanged(config: ConfigModel): void {
        
    }

    protected async Initialize(): Promise<void> {
        
    }

    protected async Destroy(): Promise<void> {
        
    }
}