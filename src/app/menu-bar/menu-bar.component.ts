import { Component } from '@angular/core';
import { BaseComponent } from '@shared/base.component';
import { Commands } from '@common/index';

@Component({
    selector: 'menu-bar',
    templateUrl: './menu-bar.component.html'
})
export class MenuBarComponent extends BaseComponent {
    readonly Commands = Commands;

    protected Initialize(): void {

    }

    protected Destroy(): void {

    }

    OnMenuClicked(menu: Commands): void {
        
    }
}