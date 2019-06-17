import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { EasyUIModule } from 'ng-easyui/components/easyui/easyui.module';

import { ComponentHostDirective } from './shared/component-host.directive';
import { SettingsComponent } from './settings/settings.component';
import { FileSystemBrowserModule } from './fs-browser/fs-browser.module'
import { SettingsModule } from './settings/settings.module';
import { MenuBarModule } from './menu-bar/menu-bar.module';
import { AppComponent } from './app.component';

import { IpcService } from '@services/ipc.service';
import { ConfigService } from '@services/config.service';
import { CommandService } from '@services/command.service';
import { PopupService } from '@services/popup.service';


@NgModule({
  declarations: [
    ComponentHostDirective,
    AppComponent
  ],
  entryComponents: [
    SettingsComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    CommonModule,
    FormsModule,
    EasyUIModule,
    FileSystemBrowserModule,
    SettingsModule,
    MenuBarModule
  ],
  providers: [
    IpcService,
    ConfigService,
    CommandService,
    PopupService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
