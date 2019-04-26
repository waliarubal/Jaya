import { NgModule } from '@angular/core';
import { TreeModule } from 'primeng/tree'
import { TableModule } from 'primeng/table'

import { FileSystemTreeComponent } from './fs-tree/fs-tree.component';
import { FileSystemTableComponent } from './fs-table/fs-table.component';
import { FileSystemBrowserComponent } from './fs-browser.component';

@NgModule({
    declarations: [
        FileSystemBrowserComponent,
        FileSystemTreeComponent,
        FileSystemTableComponent
    ],
    imports: [
        TreeModule,
        TableModule
    ],
    exports: [FileSystemBrowserComponent]
})
export class FileSystemBrowserModule {

}