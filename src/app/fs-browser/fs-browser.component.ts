import { Component } from '@angular/core';
import { BaseComponent } from '@shared/base.component';
import { DirectoryModel } from '@common/index';

@Component({
    selector: 'app-fs-browser',
    templateUrl: './fs-browser.component.html'
})
export class FileSystemBrowserComponent extends BaseComponent {
    private _directory: DirectoryModel;

    get Directory(): DirectoryModel {
        return this._directory;
    }

    set Directory(value: DirectoryModel) {
        this._directory = value;
    }

    protected Initialize(): void {
        
    }

    protected Destroy(): void {
        
    }

}