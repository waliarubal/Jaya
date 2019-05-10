import { Component, ViewChild, AfterViewInit } from '@angular/core';
import { BaseComponent } from '@shared/base.component';
import { DirectoryModel, ProviderModel } from '@common/index';
import { FileSystemService } from '@services/file-system.service';

@Component({
    selector: 'app-fs-browser',
    templateUrl: './fs-browser.component.html',
    providers: [FileSystemService]
})
export class FileSystemBrowserComponent extends BaseComponent implements AfterViewInit {
    private _directory: DirectoryModel;
    private _providers: ProviderModel[];

    constructor(private _fileSystemService: FileSystemService) {
        super();
    }

    get Directory(): DirectoryModel {
        return this._directory;
    }

    set Directory(value: DirectoryModel) {
        this._directory = value;
    }

    get Providers(): ProviderModel[] {
        return this._providers;
    }

    async ngAfterViewInit(): Promise<void> {
        this._providers = await this.GetProviders();
    }

    protected Initialize(): void {

    }

    protected Destroy(): void {

    }

    private async GetProviders(): Promise<ProviderModel[]> {
        try {
            let fileSystemProvider = await this._fileSystemService.GetProvider();
            return [fileSystemProvider];
        } catch (ex) {
            console.log(ex);
        }
    }

}