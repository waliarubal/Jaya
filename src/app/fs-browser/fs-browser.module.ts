import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EasyUIModule } from 'ng-easyui/components/easyui/easyui.module';

import { FileSystemTreeComponent } from './fs-tree/fs-tree.component';
import { FileSystemTableComponent } from './fs-table/fs-table.component';
import { FileSystemBrowserComponent } from './fs-browser.component';
import { FileSystemPreviewComponent } from './fs-preview/fs-preview.component';
import { FileSystemDetailComponent } from './fs-detail/fs-detail.component';
import { from } from 'rxjs';

@NgModule({
    declarations: [
        FileSystemBrowserComponent,
        FileSystemTreeComponent,
        FileSystemTableComponent,
        FileSystemPreviewComponent,
        FileSystemDetailComponent
    ],
    imports: [
        CommonModule,
        EasyUIModule
    ],
    exports: [FileSystemBrowserComponent]
})
export class FileSystemBrowserModule {

}