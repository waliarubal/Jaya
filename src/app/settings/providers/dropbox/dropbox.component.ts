import { Component } from '@angular/core';
import { BaseComponent } from '@shared/base.component';
import { ConfigModel, ProviderType } from '@common/index';
import { ConfigService } from '@services/config.service';
import { PopupService } from '@services/popup.service';
import { ProviderService } from '@services/provider.service';
import { DropboxService } from '@services/providers/dropbox.service';

@Component({
    selector: 'dropbox-setting',
    templateUrl: './dropbox.component.html',
    providers: [ProviderService]
})
export class DropboxComponent extends BaseComponent {
    private readonly _service: DropboxService;

    constructor(config: ConfigService, provider: ProviderService, private readonly _popupService: PopupService) {
        super(config);
        this._service = <DropboxService>provider.GetService(ProviderType.Dropbox);
    }

    protected OnConfigChanged(config: ConfigModel): void {

    }

    protected async Initialize(): Promise<void> {

    }

    protected async Destroy(): Promise<void> {

    }

    async Authenticate(): Promise<void> {
        const account = await this._service.Authenticate();
        console.log(account);
    }
}