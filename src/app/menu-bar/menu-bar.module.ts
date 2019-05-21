import { NgModule } from '@angular/core';
import { EasyUIModule } from 'ng-easyui/components/easyui/easyui.module';

import { MenuBarComponent } from './menu-bar.component';
import { ConfigService } from '@services/config.service';

@NgModule({
    declarations: [MenuBarComponent],
    imports: [EasyUIModule],
    providers: [ConfigService],
    exports: [MenuBarComponent]
})
export class MenuBarModule {

}