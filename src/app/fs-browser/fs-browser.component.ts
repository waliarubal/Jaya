import { Component, AfterViewInit } from '@angular/core';
import { BaseComponent } from '@shared/base.component';
import { DirectoryModel, ProviderModel, ConfigModel, CommandType } from '@common/index';
import { FileSystemService } from '@services/file-system.service';
import { DropboxService } from '@services/dropbox.service';
import { ConfigService } from '@services/config.service';

@Component({
    selector: 'app-fs-browser',
    templateUrl: './fs-browser.component.html',
    providers: [FileSystemService, DropboxService]
})
export class FileSystemBrowserComponent extends BaseComponent implements AfterViewInit {
    private _directory: DirectoryModel;
    private _providers: ProviderModel[];
    private _isNavigationPaneVisible: boolean;

    constructor(config: ConfigService, private _fileSystemService: FileSystemService, private _dropboxService: DropboxService) {
        super(config);
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

    get IsNavigationPaneVisible(): boolean {
        return this._isNavigationPaneVisible;
    }

    async ngAfterViewInit(): Promise<void> {
        this._providers = await this.GetProviders();
    }

    protected async Initialize(): Promise<void> {
        this._isNavigationPaneVisible = await this._config.GetValue<boolean>(CommandType.NavigationPane, false);
    }

    protected async Destroy(): Promise<void> {
        
    }

    protected OnConfigChanged(config: ConfigModel): void {
        switch(config.Key) {
            case CommandType.NavigationPane:
                this._isNavigationPaneVisible = <boolean>config.Value;
                break;
        }
    }

    private async GetProviders(): Promise<ProviderModel[]> {
        try {
            let fileSystemProvider = await this._fileSystemService.GetProvider();
            let dropboxProvider = await this._dropboxService.GetProvider();

            return [fileSystemProvider, dropboxProvider];
        } catch (ex) {
            console.log(ex);
        }
    }
}