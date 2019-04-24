import { Injectable, EventEmitter } from '@angular/core';
import { WebBaseService } from '@services/web-base.service';
import { MessageModel, MessageType, Constants } from '@common/index';

@Injectable()
export class IpcService extends WebBaseService {
    private readonly _event: EventEmitter<MessageModel>;

    constructor() {
        super();

        this._event = new EventEmitter();
        if (this.Ipc) {
            this.Ipc.on(Constants.IPC_CHANNEL, this.OnMessage);
            this.Send(MessageType.Handshake);
        }
    }

    get Receive(): EventEmitter<MessageModel> {
        return this._event;
    }

    Send(message: MessageModel): void;
    Send(type: MessageType, data?: any): void;
    Send(typeOrMessage: MessageType | MessageModel, data?: any): void {
        if (this.Ipc) {
            let messageString: string;
            if (typeOrMessage instanceof MessageModel)
                messageString = JSON.stringify(typeOrMessage);
            else {
                let message = new MessageModel(typeOrMessage, data);
                messageString = JSON.stringify(message);
            }
            this.Ipc.send(Constants.IPC_CHANNEL, messageString);
        }
        else
            console.error("Couldn't send message as electron IPC is not loaded.");
    }

    protected Dispose(): void {
        this._event.isStopped = true;
        if (this.Ipc)
            this.Ipc.removeListener(Constants.IPC_CHANNEL, this.OnMessage);
    }

    private OnMessage(event: any, messageString: string): void {
        let message = Object.assign(MessageModel.Empty(), JSON.parse(messageString));
        this.Receive.emit(message);
    }

}