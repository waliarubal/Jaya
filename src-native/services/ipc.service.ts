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

    Send(message: MessageModel): void
    Send(type: MessageType, data?: any): void;
    Send(typeOrMessage: MessageType | MessageModel, data?: any): void {
        let messageString: string;
        if (typeOrMessage instanceof MessageModel) {
            messageString = JSON.stringify(typeOrMessage);
        } else {
            let message = new MessageModel(typeOrMessage, data);
            messageString = JSON.stringify(message);
        }
        this._window.webContents.send(Constants.IPC_CHANNEL, messageString);
    }

    protected Dispose(): void {
        ipcMain.removeAllListeners(Constants.IPC_CHANNEL);
        this._event.removeAllListeners(Constants.IPC_CHANNEL);
    }

    private OnMessage(event: any, messageString: string): void {
        let message = Object.assign(MessageModel.Empty(), JSON.parse(messageString));
        this._event.emit(Constants.IPC_CHANNEL, message);
    }
}