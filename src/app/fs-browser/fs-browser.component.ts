import { Component, AfterViewInit } from '@angular/core';
import { BaseComponent } from '@shared/base.component';
import { DirectoryModel, ProviderModel, ConfigModel, CommandType, IFileSystemObject, Constants, Helpers, ProviderType } from '@common/index';
import { ConfigService } from '@services/config.service';
import { ProviderService } from '@services/provider.service';

@Component({
    selector: 'app-fs-browser',
    templateUrl: './fs-browser.component.html',
    providers: [ProviderService]
})
export class FileSystemBrowserComponent extends BaseComponent implements AfterViewInit {
    private _directory: DirectoryModel;
    private _providers: ProviderModel[];
    private _selectedObjects: IFileSystemObject[];
    private _isNavigationPaneVisible: boolean;
    private _isPreviewPaneVisible: boolean;
    private _isDetailsPaneVisible: boolean;

    constructor(config: ConfigService, private readonly _providerService: ProviderService) {
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

    ngAfterViewInit(): void {
        this.Refresh().then();
    }

    private async Refresh(): Promise<void> {
        let providers = await this._providerService.GetProviders();
        for (let provider of providers)
            provider.IsEnabled = Helpers.ToBoolean(await this._config.GetValue(provider.Type, Constants.FALSE));

        this._providers = providers
    }

    protected async Initialize(): Promise<void> {
        this._isNavigationPaneVisible = Helpers.ToBoolean(await this._config.GetValue(CommandType.NavigationPane, Constants.TRUE));
        this._isPreviewPaneVisible = Helpers.ToBoolean(await this._config.GetValue(CommandType.PreviewPane, Constants.FALSE));
        this._isDetailsPaneVisible = Helpers.ToBoolean(await this._config.GetValue(CommandType.DetailsPane, Constants.FALSE));
    }

    protected async Destroy(): Promise<void> {

    }

    protected OnConfigChanged(config: ConfigModel): void {
        switch (config.Key) {
            case CommandType.NavigationPane:
                this._isNavigationPaneVisible = Helpers.ToBoolean(config.Value);
                break;

            case CommandType.PreviewPane:
                this._isPreviewPaneVisible = Helpers.ToBoolean(config.Value);
                if (this.IsPreviewPaneVisible && this.IsDetailsPaneVisible) {
                    this._isDetailsPaneVisible = false;
                    this._config.SetValue(CommandType.DetailsPane, this.IsDetailsPaneVisible.toString()).then();
                }
                break;

            case CommandType.DetailsPane:
                this._isDetailsPaneVisible = Helpers.ToBoolean(config.Value);
                if (this.IsDetailsPaneVisible && this.IsPreviewPaneVisible) {
                    this._isPreviewPaneVisible = false;
                    this._config.SetValue(CommandType.PreviewPane, this.IsPreviewPaneVisible.toString()).then();
                }
                break;

            case ProviderType.FileSystem:
            case ProviderType.Dropbox:
            case ProviderType.GoogleDrive:
                alert('here');
                this.Refresh().then();
                break;
        }
    }
}