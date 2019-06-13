import { Component, AfterViewInit } from '@angular/core';
import { BaseComponent } from '@shared/base.component';
import { DirectoryModel, ProviderModel, ConfigModel, CommandType, IFileSystemObject } from '@common/index';
import { FileSystemService } from '@services/providers/file-system.service';
import { DropboxService } from '@services/providers/dropbox.service';
import { ConfigService } from '@services/config.service';

@Component({
    selector: 'app-fs-browser',
    templateUrl: './fs-browser.component.html',
    providers: [FileSystemService, DropboxService]
})
export class FileSystemBrowserComponent extends BaseComponent implements AfterViewInit {
    private _directory: DirectoryModel;
    private _providers: ProviderModel[];
    private _selectedObjects: IFileSystemObject[];
    private _isNavigationPaneVisible: boolean;
    private _isPreviewPaneVisible: boolean;
    private _isDetailsPaneVisible: boolean;

    constructor(config: ConfigService, private _fileSystemService: FileSystemService, private _dropboxService: DropboxService) {
        super(config);
    }

    get Directory(): DirectoryModel {
        return this._directory;
    }

    set Directory(value: DirectoryModel) {
        this._directory = value;
    }

    get SelectedObjects(): IFileSystemObject[] {
        return this._selectedObjects;
    }

    set SelectedObjects(value: IFileSystemObject[]) {
        this._selectedObjects = value;
    }

    get Providers(): ProviderModel[] {
        return this._providers;
    }

    get IsNavigationPaneVisible(): boolean {
        return this._isNavigationPaneVisible;
    }

    get IsPreviewPaneVisible(): boolean {
        return this._isPreviewPaneVisible;
    }

    get IsDetailsPaneVisible(): boolean {
        return this._isDetailsPaneVisible;
    }

    async ngAfterViewInit(): Promise<void> {
        this._providers = await this.GetProviders();
    }

    protected async Initialize(): Promise<void> {
        this._isNavigationPaneVisible = await this._config.GetValue<boolean>(CommandType.NavigationPane, true);
        this._isPreviewPaneVisible = await this._config.GetValue<boolean>(CommandType.PreviewPane, false);
        this._isDetailsPaneVisible = await this._config.GetValue<boolean>(CommandType.DetailsPane, false);
    }

    protected async Destroy(): Promise<void> {

    }

    protected OnConfigChanged(config: ConfigModel): void {
        switch (config.Key) {
            case CommandType.NavigationPane:
                this._isNavigationPaneVisible = <boolean>config.Value;
                break;

            case CommandType.PreviewPane:
                this._isPreviewPaneVisible = <boolean>config.Value;
                if (this.IsPreviewPaneVisible && this.IsDetailsPaneVisible) {
                    this._isDetailsPaneVisible = false;
                    this._config.SetValue(CommandType.DetailsPane, this.IsDetailsPaneVisible).then();
                }
                break;

            case CommandType.DetailsPane:
                this._isDetailsPaneVisible = <boolean>config.Value;
                if (this.IsDetailsPaneVisible && this.IsPreviewPaneVisible) {
                    this._isPreviewPaneVisible = false;
                    this._config.SetValue(CommandType.PreviewPane, this.IsPreviewPaneVisible).then();
                }
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