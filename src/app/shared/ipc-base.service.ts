import { IpcRenderer } from 'electron';
import { OnDestroy, EventEmitter } from '@angular/core';
import { Constants, BaseService, MessageModel, MessageType } from '@common/index';

export abstract class IpcBaseService extends BaseService implements OnDestroy {
    private readonly _ipc: IpcRenderer;
    private readonly _event: EventEmitter<MessageModel>;

    constructor() {
        super();

        this._event = new EventEmitter();
        if ((<any>window).require) {
            try {
                let ipc = this._ipc = (<any>window).require('electron').ipcRenderer;
                if (ipc) {
                    ipc.on(Constants.IPC_CHANNEL, (event: any, args: any) => this.OnMessage(event, args));
                    this.Send(MessageType.Handshake);
                }
            } catch (e) {
                console.error(e);
            }
        } else
            console.error('Could not load electron IPC.');
    }

    get Receive(): EventEmitter<MessageModel> {
        return this._event;
    }

    Send(message: MessageModel): void;
    Send(type: MessageType, data?: any): void;
    Send(typeOrMessage: MessageType | MessageModel, data?: any): void {
        if (this._ipc) {
            let messageString: string;
            if (typeOrMessage instanceof MessageModel)
                messageString = JSON.stringify(typeOrMessage);
            else {
                let message = MessageModel.New(typeOrMessage, data);
                messageString = JSON.stringify(message);
            }
            this._ipc.send(Constants.IPC_CHANNEL, messageString);
        }
        else
            console.error("Couldn't send message as electron IPC is not loaded.");
    }

    ngOnDestroy(): void {
        this._event.isStopped = true;
        if (this._ipc)
            this._ipc.removeAllListeners(Constants.IPC_CHANNEL);

        this.Dispose();
    }

    private OnMessage(event: any, messageString: string): void {
        let message = <MessageModel>Object.assign(MessageModel.Empty(), JSON.parse(messageString));
        switch (message.Type) {
            case MessageType.Unknown:
                return;

            case MessageType.Handshake:
                console.log(`[${message.Id}] IPC bridge created.`);
                break;

            default:
                this.Receive.emit(message);
                break;
        }
    }

}