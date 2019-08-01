import { Component } from '@angular/core';
import { BaseComponent } from '@shared/base.component';
import { ConfigModel, ProviderType, Dictionary, ProviderModel, Constants, Helpers } from '@common/index';
import { ConfigService } from '@services/config.service';
import { ProviderService } from '@services/provider.service';

@Component({
    selector: 'app-settings',
    templateUrl: './settings.component.html',
    providers: [ProviderService]
})
export class SettingsComponent extends BaseComponent {
    private _providers: ProviderModel[];
    readonly ProviderType = ProviderType;
    readonly Offset = ProviderType.FileSystem;

    constructor(config: ConfigService, private readonly _providerService: ProviderService) {
        super(config);
    }

    get Providers(): ProviderModel[] {
        return this._providers;
    }

    protected OnConfigChanged(config: ConfigModel): void {

    }

    protected async Initialize(): Promise<void> {
        this._providers = await this._providerService.GetProviders();
        for (let provider of this.Providers)
            provider.IsEnabled = Helpers.ToBoolean(await this._config.GetValue(provider.Type, Constants.FALSE));
    }

    protected async Destroy(): Promise<void> {
        for (let provider of this.Providers)
            await this._config.SetValue(provider.Type, provider.IsEnabled.toString());
    }
}