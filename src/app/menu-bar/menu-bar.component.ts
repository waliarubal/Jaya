import { Component } from '@angular/core';
import { BaseComponent } from '@shared/base.component';
import { Constants, CommandType, IMenu } from '@common/index';
import { ConfigService } from '@services/config.service';
import { CommandService } from '@services/command.service';

@Component({
    selector: 'menu-bar',
    templateUrl: './menu-bar.component.html'
})
export class MenuBarComponent extends BaseComponent {
    readonly Menus = Constants.MENU_DATA;

    IsItemCheckBoxVisible: boolean;

    constructor(
        private readonly _config: ConfigService,
        private readonly _command: CommandService) {
        super();
    }

    protected async Initialize(): Promise<void> {
        for (let menu of this.Menus) {
            if (!menu.SubMenus)
                continue;

            for (let subMenu of menu.SubMenus) {
                if (!subMenu.IsCheckable)
                    continue;

                subMenu.IsChecked = await this._config.GetOrSetConfiguration<boolean>(subMenu.Command, false);
            }
        }
    }

    protected async Destroy(): Promise<void> {
        
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

        if (menu.IsCheckable) {
            menu.IsChecked = !menu.IsChecked;
            await this._config.GetOrSetConfiguration(menu.Command, menu.IsChecked);
        }
        
        switch (menu.Command) {
            case CommandType.Exit:
                this._command.Execute(CommandType.Exit, null);
                break;

            case CommandType.ItemCheckBoxes:        
                break;

        }
    }
} 