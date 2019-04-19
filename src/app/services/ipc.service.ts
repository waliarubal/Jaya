import { WebBaseService } from '@services/web-base.service';
import { MessageModel } from '@shared/models/message.model';
import { Constants } from '@shared/constants';
import { Injectable } from '@angular/core';

@Injectable()
export class IpcService extends WebBaseService {

    Send(message: MessageModel) {
        this.Ipc.send(Constants.IPC_CHANNEL, message);
    }

    protected Dispose(): void {

    }

}