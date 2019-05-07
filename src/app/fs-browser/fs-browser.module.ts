import { NgModule } from '@angular/core';
import { TreeViewModule } from '@progress/kendo-angular-treeview';

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
        TreeViewModule
    ],
    exports: [FileSystemBrowserComponent]
})
export class FileSystemBrowserModule {

}