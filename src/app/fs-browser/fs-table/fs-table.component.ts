import { Component, Input } from '@angular/core';
import { BaseComponent } from '@shared/base.component';
import { DirectoryModel, FileModel } from '@common/index';

@Component({
    selector: 'app-fs-table',
    templateUrl: './fs-table.component.html'
})
export class FileSystemTableComponent extends BaseComponent {
    private _directory: DirectoryModel;
    private _contents: FileModel[];

    protected Initialize(): void {
        this._contents = [];
    }

    protected Destroy(): void {

    }

    @Input()
    get Directory(): DirectoryModel {
        return this._directory;
    }

    set Directory(value: DirectoryModel) {
        this._directory = value;

        if (value) {
            let contents: FileModel[] = [];
            for (let directory of value.Directories)
                contents.push(directory);
            for (let file of value.Files)
                contents.push(file);
            this._contents = contents;
        }
    }

    get Contents(): FileModel[] {
        return this._contents;
    }
}