import { NgModule } from '@angular/core';
import { jqxTreeComponent } from 'jqwidgets-scripts/jqwidgets-ts/angular_jqxtree'

import { FileSystemTreeComponent } from './fs-tree/fs-tree.component';
import { FileSystemTableComponent } from './fs-table/fs-table.component';
import { FileSystemBrowserComponent } from './fs-browser.component';

@NgModule({
    declarations: [
        jqxTreeComponent,
        FileSystemBrowserComponent,
        FileSystemTreeComponent,
        FileSystemTableComponent
    ],
    imports: [

    ],
    exports: [FileSystemBrowserComponent]
})
export class FileSystemBrowserModule {

}