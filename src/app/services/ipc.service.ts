import { MessageModel } from '@shared/models/message.model';
import { WebBaseService } from '@services/web-base.service';

export class IpcService extends WebBaseService {

    Send(message: MessageModel) {
        this.Ipc.send(this.IPC_CHANNEL, message);
    }
}