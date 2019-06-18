import { Component } from '@angular/core';
import { BaseComponent } from '@shared/base.component';
import { ConfigModel } from '@common/index';
import { ConfigService } from '@services/config.service';

@Component({
    selector: 'app-settings',
    templateUrl: './settings.component.html'
})
export class SettingsComponent extends BaseComponent {

    constructor(config: ConfigService) {
        super(config);
    }

    get IsDropboxEnabled(): boolean {
        return this.Get('is_d');
    }

    set IsDropboxEnabled(value: boolean) {
        this.Set('is_d', value);
    }

    protected OnConfigChanged(config: ConfigModel): void {

    }

    protected async Initialize(): Promise<void> {

    }

    protected async Destroy(): Promise<void> {

    }



}