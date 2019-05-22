import { Component } from '@angular/core';
import { BaseComponent } from '@shared/base.component';
import { Commands } from '@common/index';
import { ConfigService } from '@services/config.service';

@Component({
    selector: 'menu-bar',
    templateUrl: './menu-bar.component.html'
})
export class MenuBarComponent extends BaseComponent {

    readonly Commands = Commands;

    IsItemCheckBoxVisible: boolean;

    constructor(private readonly _config: ConfigService) {
        super();
    }

    protected async Initialize(): Promise<void> {
        this.IsItemCheckBoxVisible = await this._config.GetOrSetConfiguration<boolean>(Commands.ItemCheckBoxes, false);
    }

    protected async Destroy(): Promise<void> {
        await this._config.GetOrSetConfiguration(Commands.ItemCheckBoxes, this.IsItemCheckBoxVisible);
    }

    OnMenuClicked(): void {

    }
}