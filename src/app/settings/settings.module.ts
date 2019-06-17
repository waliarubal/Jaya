import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EasyUIModule } from 'ng-easyui/components/easyui/easyui.module';

import { SettingsComponent } from './settings.component';
import { DropboxComponent } from './providers/dropbox/dropbox.component';

@NgModule({
    declarations: [
        SettingsComponent,
        DropboxComponent
    ],
    imports: [
        CommonModule,
        EasyUIModule
    ],
    exports: [SettingsComponent]
})
export class SettingsModule {

}