import { NgModule } from '@angular/core';

import { ControlsModule } from '@shared/controls/controls.module';
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
        ControlsModule
    ],
    exports: [FileSystemBrowserComponent]
})
export class FileSystemBrowserModule {

}