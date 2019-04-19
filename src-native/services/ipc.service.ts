import { ipcMain, BrowserWindow, EventEmitter } from 'electron';
import { Constants } from '@shared/constants';
import { BaseService } from '@shared/base.service'
import { MessageModel } from '@shared/models/message.model';

export class IpcService extends BaseService {
    private readonly _window: BrowserWindow;
    private readonly _event: EventEmitter;

    constructor(window: BrowserWindow){
        super();
        this._event = new EventEmitter();
        this._window = window;
        
        ipcMain.on(Constants.IPC_CHANNEL, (event: any, message: MessageModel) => this.Event.emit(Constants.IPC_CHANNEL, message));
    }

    get Event(): EventEmitter {
        return this._event;
    }

    protected Dispose(): void {
        this._event.removeAllListeners(Constants.IPC_CHANNEL)
        ipcMain.removeAllListeners(Constants.IPC_CHANNEL);
    }

    Stop(): void {
        this.Dispose();
    }

    Send(message: MessageModel): void {
        this._window.webContents.send(Constants.IPC_CHANNEL, message);
    }
}