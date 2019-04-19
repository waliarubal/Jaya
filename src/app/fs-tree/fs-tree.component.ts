import { Component } from '@angular/core';
import { BaseComponent } from '../base.component';
import { MessageModel } from '@shared/models/message.model';

@Component({
    selector: 'app-fs-tree',
    templateUrl: './fs-tree.component.html'
})
export class FileSystemTreeComponent extends BaseComponent {

    protected OnMessage(message: MessageModel): void {
        console.log(message);
    }

}