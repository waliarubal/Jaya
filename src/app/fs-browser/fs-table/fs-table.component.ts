import { Component, Input } from '@angular/core';
import { BaseComponent } from '@shared/base.component';
import { DirectoryModel } from '@common/index';

@Component({
    selector: 'app-fs-table',
    templateUrl: './fs-table.component.html'
})
export class FileSystemTableComponent extends BaseComponent {
    private _directory: DirectoryModel;

    protected Initialize(): void {

    }

    protected Destroy(): void {

    }

    @Input()
    get Directory(): DirectoryModel {
        return this._directory;
    }

    set Directory(value: DirectoryModel) {
        this._directory = value;
    }
}