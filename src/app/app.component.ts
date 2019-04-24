import { Component } from '@angular/core';
import { IpcService } from '@services/ipc.service';
import { BaseComponent } from '@shared/base.component';
import { MessageModel, MessageType } from '@common/index';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent extends BaseComponent {

  constructor(ipc: IpcService) {
    super(ipc);
  }

  protected ReceiveMessage(message: MessageModel): void {
    console.log(message);
  }

}
