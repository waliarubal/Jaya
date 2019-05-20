import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { EasyUIModule } from 'ng-easyui/components/easyui/easyui.module';

import { FileSystemBrowserModule } from './fs-browser/fs-browser.module'
import { FileSystemProviderModule } from './fs-provider/fs-provider.modeule';
import { AppComponent } from './app.component';
import { MenuBarComponent } from './menu-bar/menu-bar.component';
import { IpcService } from '@services/ipc.service';

@NgModule({
  declarations: [
    AppComponent,
    MenuBarComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    CommonModule,
    FormsModule,
    EasyUIModule,
    FileSystemBrowserModule,
    FileSystemProviderModule
  ],
  providers: [IpcService],
  bootstrap: [AppComponent]
})
export class AppModule { }
