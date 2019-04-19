import { Component } from '@angular/core';
import { BaseComponent } from '../base.component';
import { MessageModel } from '@shared/models/message.model';
import { ElectronService } from 'ngx-electron';

@Component({
    selector: 'app-fs-tree',
    templateUrl: './fs-tree.component.html'
})
export class FileSystemTreeComponent extends BaseComponent {

    constructor(electron: ElectronService) {
        super(electron);
    }

    protected OnMessage(message: MessageModel): void {
        console.log(message);
    }

}