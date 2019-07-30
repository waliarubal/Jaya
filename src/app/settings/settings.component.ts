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

    constructor(config: ConfigService, private readonly _providerService: ProviderService) {
        super(config);
    }

    get Providers(): ProviderModel[] {
        return this._providers;
    }

    protected OnConfigChanged(config: ConfigModel): void {

    }

    protected async Initialize(): Promise<void> {
        const providers = await this._providerService.GetProviders();
        let models: ProviderModel[] = [];
        for (let provider of providers) {
            provider.IsEnabled = Helpers.ToBoolean(await this._config.GetValue(provider.Type, Constants.FALSE));
            models[provider.Type] = provider;
        }
        this._providers = models;
    }

    protected async Destroy(): Promise<void> {

    }
}