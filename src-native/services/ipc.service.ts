import { ipcMain, BrowserWindow } from 'electron';
import { Constants } from '@shared/constants';
import { BaseService } from '@shared/base.service'
import { MessageModel } from '@shared/models/message.model';

export abstract class IpcService extends BaseService {
    private readonly _window: BrowserWindow;

    constructor(window: BrowserWindow){
        super();
        this._window = window;
        
        ipcMain.on(Constants.IPC_CHANNEL, (event: any, message: MessageModel) => this.Receive(message));
    }

    protected Dispose(): void {
        ipcMain.removeAllListeners(Constants.IPC_CHANNEL);
    }

    protected abstract Receive(message: MessageModel): void;

    protected Send(message: MessageModel): void {
        this._window.webContents.send(Constants.IPC_CHANNEL, message);
    }
}