import { Component, Input, EventEmitter, Output } from '@angular/core';
import { BaseComponent } from '@shared/base.component';
import { DirectoryModel, IFileSystemObject, ConfigModel } from '@common/index';
import { ConfigService } from '@services/config.service';

@Component({
    selector: 'app-fs-table',
    templateUrl: './fs-table.component.html'
})
export class FileSystemTableComponent extends BaseComponent {
    private readonly _onObjectsSelected: EventEmitter<IFileSystemObject[]>;
    private _directory: DirectoryModel;
    private _objects: IFileSystemObject[];

    constructor(config: ConfigService) {
        super(config);
        this._onObjectsSelected = new EventEmitter();
    }

    @Output()
    get OnObjectsSelected(): EventEmitter<IFileSystemObject[]> {
        return this._onObjectsSelected;
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

    protected async Initialize(): Promise<void> {

    }

    protected async Destroy(): Promise<void> {

    }

    protected OnConfigChanged(config: ConfigModel): void {

    }


}