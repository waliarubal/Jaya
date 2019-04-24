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
            this.Ipc.on(Constants.IPC_CHANNEL, (event: any, argument: any) => this.Receive.emit(argument));
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
            if (typeOrMessage instanceof MessageModel)
                this.Ipc.send(Constants.IPC_CHANNEL, typeOrMessage);
            else {
                let message = new MessageModel(typeOrMessage, data);
                this.Ipc.send(Constants.IPC_CHANNEL, message);
            }
        }
        else
            console.error("Couldn't send message as electron IPC is not loaded.");
    }

    protected Dispose(): void {
        this._event.isStopped = true;
        if (this.Ipc)
            this.Ipc.removeAllListeners(Constants.IPC_CHANNEL);
    }

}