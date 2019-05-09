import { NgModule } from '@angular/core';
import { EasyUIModule } from 'ng-easyui/components/easyui/easyui.module';

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
        EasyUIModule
    ],
    exports: [FileSystemBrowserComponent]
})
export class FileSystemBrowserModule {

}