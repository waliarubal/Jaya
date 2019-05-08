import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TreeComponent } from './tree/tree.component';

@NgModule({
    declarations: [TreeComponent],
    imports: [CommonModule],
    exports: [TreeComponent]
})
export class ControlsModule {

}