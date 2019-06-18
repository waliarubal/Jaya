import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
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
        FormsModule,
        EasyUIModule
    ],
    exports: [SettingsComponent]
})
export class SettingsModule {

}