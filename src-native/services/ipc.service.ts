import { ipcMain, BrowserWindow } from 'electron';
import { Constants, BaseService, MessageModel } from '../../src-common'

export abstract class IpcService extends BaseService {

    constructor(readonly _window: BrowserWindow){
        super();
        ipcMain.on(Constants.IPC_CHANNEL, (event: any, message: MessageModel) => this.Receive(message));
    }

    protected Dispose(): void {
        ipcMain.removeAllListeners(Constants.IPC_CHANNEL);
    }

    protected Send(message: MessageModel): void {
        this._window.webContents.send(Constants.IPC_CHANNEL, message);
    }

    protected abstract Receive(message: MessageModel): void;
}