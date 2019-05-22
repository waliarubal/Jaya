import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EasyUIModule } from 'ng-easyui/components/easyui/easyui.module';

import { MenuBarComponent } from './menu-bar.component';

@NgModule({
    declarations: [MenuBarComponent],
    imports: [
        CommonModule,
        EasyUIModule
    ],
    exports: [MenuBarComponent]
})
export class MenuBarModule {

}