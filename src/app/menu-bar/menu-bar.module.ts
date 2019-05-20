import { NgModule } from '@angular/core';
import { EasyUIModule } from 'ng-easyui/components/easyui/easyui.module';

import { MenuBarComponent } from './menu-bar.component';

@NgModule({
    declarations: [MenuBarComponent],
    imports: [EasyUIModule],
    exports: [MenuBarComponent]
})
export class MenuBarModule {

}