import { Component, Input } from '@angular/core';
import { BaseComponent } from '@shared/base.component';
import { DirectoryModel, FileModel, IFileSystemObject } from '@common/index';

@Component({
    selector: 'app-fs-table',
    templateUrl: './fs-table.component.html'
})
export class FileSystemTableComponent extends BaseComponent {
    private _directory: DirectoryModel;
    private _objects: IFileSystemObject[];

    protected Initialize(): void {
        //this._objects = [];
    }

    protected Destroy(): void {

    }

    @Input()
    get Directory(): DirectoryModel {
        return this._directory;
    }

    set Directory(value: DirectoryModel) {
        this._directory = value;
        if (value)
            this._objects = value.GetObjects();
    }

    get Objects(): IFileSystemObject[] {
        return this._objects;
    }
}