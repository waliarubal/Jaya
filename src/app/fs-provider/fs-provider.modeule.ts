import { NgModule } from '@angular/core';
import { EasyUIModule } from 'ng-easyui/components/easyui/easyui.module';

import { ComputerComponent } from './computer/computer.component';

@NgModule({
    declarations: [
        ComputerComponent
    ],
    imports: [EasyUIModule],
    exports: [
        ComputerComponent
    ]
})
export class FileSystemProviderModule {

}