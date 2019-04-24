import { ipcMain, BrowserWindow } from 'electron';
import { EventEmitter } from 'events';
import { Constants, BaseService, MessageModel, MessageType } from '../../src-common'

export class IpcService extends BaseService {
    private readonly _event: EventEmitter;

    constructor(readonly _window: BrowserWindow) {
        super();
        this._event = new EventEmitter();
        ipcMain.on(Constants.IPC_CHANNEL, (event: any, argument: any) => this.OnMessage(event, argument));
    }

    get Receive(): EventEmitter {
        return this._event;
    }

    Send(type: MessageType, data?: string): void;
    Send(message: MessageModel): void
    Send(typeOrMessage: MessageType | MessageModel, data: string = ''): void {
        let messageString: string;
        if (typeOrMessage instanceof MessageModel) {
            messageString = JSON.stringify(typeOrMessage);
        } else {
            let message = MessageModel.New(typeOrMessage, data);
            messageString = JSON.stringify(message);
        }
        this._window.webContents.send(Constants.IPC_CHANNEL, messageString);
    }

    protected Dispose(): void {
        ipcMain.removeAllListeners(Constants.IPC_CHANNEL);
        this.Receive.removeAllListeners(Constants.IPC_CHANNEL);
    }

    private OnMessage(event: any, messageString: string): void {
        let message = <MessageModel>Object.assign(MessageModel.Empty(), JSON.parse(messageString));
        switch (message.Type) {
            case MessageType.Unknown:
                return;

            case MessageType.Handshake:
                this.Send(message);
                console.log(`[${message.Id}] IPC bridge created.`);
                break;

            default:
                this.Receive.emit(Constants.IPC_CHANNEL, message);
                break;
        }
    }
}