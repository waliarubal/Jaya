import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { TreeModule } from 'primeng/tree'
import { TableModule } from 'primeng/table'

import { AppComponent } from './app.component';
import { FileSystemTreeComponent } from './file-system-tree/file-system-tree.component';
import { FileSystemTableComponent } from './file-system-table/file-system-table.component';

@NgModule({
  declarations: [
    AppComponent,
    FileSystemTreeComponent,
    FileSystemTableComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    TreeModule,
    TableModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
