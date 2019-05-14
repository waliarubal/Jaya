import { IpcRenderer } from 'electron';
import { OnDestroy, EventEmitter, Injectable } from '@angular/core';
import { Constants, BaseService, MessageModel, MessageType } from '@common/index';

@Injectable({
    providedIn: 'root'
})
export class IpcService extends BaseService implements OnDestroy {
    private readonly _ipc: IpcRenderer;
    private readonly _event: EventEmitter<MessageModel>;

    constructor() {
        super();

        this._event = new EventEmitter();
        this._event.isStopped = true;

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

    get IsReceiving(): boolean {
        return !this._event.isStopped;
    }

    set IsReceiving(value: boolean) {
        this._event.isStopped = !value;
    }

    async SendAsync(message: MessageModel): Promise<MessageModel>;
    async SendAsync(type: MessageType, data?: string): Promise<MessageModel>;
    async SendAsync(typeOrMessage: MessageType | MessageModel, data: string = ''): Promise<MessageModel> {
        let messageString: string;
        let id: string;
        if (typeOrMessage instanceof MessageModel) {
            id = typeOrMessage.Id;
            messageString = JSON.stringify(typeOrMessage);
        }
        else {
            let message = MessageModel.New(typeOrMessage, data);
            id = message.Id;
            messageString = JSON.stringify(message);
        }

        return new Promise<MessageModel>((resolve, reject) => {
            let listener = (event: any, messageString: string) => {
                let reply = <MessageModel>Object.assign(MessageModel.Empty(), JSON.parse(messageString));
                if (reply.Id === id) {
                    this._ipc.removeListener(Constants.IPC_CHANNEL, listener);
                    resolve(reply);
                }
            };
            this._ipc.on(Constants.IPC_CHANNEL, listener);
            this._ipc.send(Constants.IPC_CHANNEL, messageString);
        });
    }

    Send(message: MessageModel): void;
    Send(type: MessageType, data?: string): void;
    Send(typeOrMessage: MessageType | MessageModel, data: string = ''): void {
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
        this.Dispose();
    }

    protected Dispose(): void {
        this._event.isStopped = true;
        if (this._ipc)
            this._ipc.removeAllListeners(Constants.IPC_CHANNEL);
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