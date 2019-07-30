import { Component } from '@angular/core';
import { BaseComponent } from '@shared/base.component';
import { Constants, CommandType, IMenu, ConfigModel, Helpers } from '@common/index';
import { ConfigService } from '@services/config.service';
import { CommandService } from '@services/command.service';
import { PopupService } from '@services/popup.service';
import { SettingsComponent } from '../settings/settings.component';

@Component({
    selector: 'menu-bar',
    templateUrl: './menu-bar.component.html'
})
export class MenuBarComponent extends BaseComponent {
    readonly Menus = Constants.MENU_DATA;

    IsItemCheckBoxVisible: boolean;

    constructor(
        config: ConfigService,
        private readonly _command: CommandService,
        private readonly _popupService: PopupService) {
        super(config);
    }

    protected async Initialize(): Promise<void> {
        // check/uncheck checkable menus
        for (let menu of this.Menus) {
            if (!menu.SubMenus)
                continue;

            for (let subMenu of menu.SubMenus) {
                if (!subMenu.IsCheckable)
                    continue;

                subMenu.IsChecked = Helpers.ToBoolean(await this._config.GetValue(subMenu.Command, Constants.FALSE));
            }
        }
    }

    protected async Destroy(): Promise<void> {

    }

    protected OnConfigChanged(config: ConfigModel): void {
        // check/uncheck checkable menu
        let menu = this.GetMenu(config.Key);
        if (menu && menu.IsCheckable)
            menu.IsChecked = Helpers.ToBoolean(config.Value);
    }

    private GetMenu(command: CommandType): IMenu {
        for (let menu of this.Menus) {
            if (menu.Command === command)
                return menu;

            if (menu.SubMenus)
                for (let subMenu of menu.SubMenus)
                    if (subMenu.Command === command)
                        return subMenu;
        }

        return null;
    }

    async OnMenuClicked(command: CommandType): Promise<void> {
        const menu = this.GetMenu(command);
        if (!menu)
            return;

        // toggle checkable menu
        if (menu.IsCheckable) {
            menu.IsChecked = !menu.IsChecked;
            await this._config.SetValue(menu.Command, menu.IsChecked.toString());
        }

        switch (menu.Command) {
            case CommandType.Exit:
                this._command.Execute(CommandType.Exit, null);
                return;

            case CommandType.Providers:
                this._popupService.Open('Settings', SettingsComponent)
                break;
        }
    }
} 