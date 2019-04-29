import { Component, ViewChild, AfterViewInit } from '@angular/core';
import { BaseComponent } from '@shared/base.component';
import { DirectoryModel } from '@common/index';
import { FileSystemTreeComponent } from './fs-tree/fs-tree.component';

@Component({
    selector: 'app-fs-browser',
    templateUrl: './fs-browser.component.html'
})
export class FileSystemBrowserComponent extends BaseComponent implements AfterViewInit {
    private _directory: DirectoryModel;
    @ViewChild("fsTree") private _fsTreeComponent: FileSystemTreeComponent;

    constructor() {
        super();
        this._directory = <DirectoryModel>{
            Name: '/',
            Path: '/'
        };
    }

    get Directory(): DirectoryModel {
        return this._directory;
    }

    set Directory(value: DirectoryModel) {
        this._directory = value;
    }

    async ngAfterViewInit(): Promise<void> {
        await this._fsTreeComponent.PopulateNode();
    }

    protected Initialize(): void {

    }

    protected Destroy(): void {

    }

}