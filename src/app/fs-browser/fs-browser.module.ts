import { NgModule } from '@angular/core';
import { jqxTreeModule } from 'jqwidgets-ng/jqxtree';
import { jqxGridModule } from 'jqwidgets-ng/jqxgrid';

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
        jqxTreeModule,
        jqxGridModule
    ],
    exports: [FileSystemBrowserComponent]
})
export class FileSystemBrowserModule {

}