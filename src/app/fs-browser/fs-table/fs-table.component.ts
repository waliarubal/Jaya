import { Component, Input } from '@angular/core';
import { BaseComponent } from '@shared/base.component';
import { DirectoryModel, FileModel } from '@common/index';

@Component({
    selector: 'app-fs-table',
    templateUrl: './fs-table.component.html'
})
export class FileSystemTableComponent extends BaseComponent {
    private _directory: string;
    private _contents: FileModel[];

    protected Initialize(): void {
        this._contents = [];
    }

    protected Destroy(): void {

    }

    @Input()
    get Directory(): string {
        return this._directory;
    }

    set Directory(value: string) {
        this._directory = value;
        console.log(value);
    }

    get Contents(): FileModel[] {
        return this._contents;
    }
}