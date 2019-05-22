import { Component } from '@angular/core';
import { BaseComponent } from '@shared/base.component';
import { Constants, CommandType } from '@common/index';
import { ConfigService } from '@services/config.service';

@Component({
    selector: 'menu-bar',
    templateUrl: './menu-bar.component.html'
})
export class MenuBarComponent extends BaseComponent {
    readonly Commands = Constants.MENU_DATA;

    IsItemCheckBoxVisible: boolean;

    constructor(private readonly _config: ConfigService) {
        super();
    }

    protected async Initialize(): Promise<void> {
        // this.IsItemCheckBoxVisible = await this._config.GetOrSetConfiguration<boolean>(CommandType.ItemCheckBoxes, false);
    }

    protected async Destroy(): Promise<void> {
        // await this._config.GetOrSetConfiguration(CommandType.ItemCheckBoxes, this.IsItemCheckBoxVisible);
    }

    OnMenuClicked(label: string): void {
        
    }
}