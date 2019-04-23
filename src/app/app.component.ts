import { Component } from '@angular/core';
import { IpcService } from '@services/ipc.service';
import { BaseComponent } from '@shared/base.component';
import { MessageModel, MessageType } from '@shared/models';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  providers: [IpcService]
})
export class AppComponent extends BaseComponent {

  constructor(ipc: IpcService) {
    super(ipc);
  }
  
  protected ReceiveMessage(message: MessageModel): void {
    console.log(message);
  }

  ngOnInit(): void {
    this.SendMessage(new MessageModel(MessageType.Handshake, 'Hello World'));
  }

}
