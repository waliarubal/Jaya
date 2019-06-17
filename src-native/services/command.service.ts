import { app, BrowserWindow } from 'electron';
import * as Url from 'url'
import { SuperService } from '../shared';
import { IpcService } from './ipc.service';
import { MessageModel, Constants, MessageType, Helpers, CommandModel, CommandType } from '../../src-common';

export class CommandService extends SuperService {

    constructor(private readonly _ipc: IpcService) {
        super();
        this._ipc.Receive.on(Constants.IPC_CHANNEL, (message: MessageModel) => this.OnMessage(message));
    }

    protected async Dispose(): Promise<void> {
        this._ipc.Receive.removeAllListeners(Constants.IPC_CHANNEL);
    }

    private OnMessage(message: MessageModel): void {
        if (message.Type !== MessageType.Command)
            return;

        const command = Helpers.Deserialize<CommandModel>(message.DataJson, CommandModel);
        switch (command.Command) {
            case CommandType.Exit:
                app.quit();
                break;

            case CommandType.OpenWindow:
                const url = Url.format(command.Parameter);
                let window = new BrowserWindow({
                    modal: true,
                    parent: this._ipc.Window,
                    maximizable: false,
                    focusable: true,
                    skipTaskbar: true,
                    useContentSize: true,
                    title: 'Loading...',
                    webPreferences: {
                        contextIsolation: true,
                        nodeIntegration: false
                    }
                });
                window.webContents.once('dom-ready', () => window.webContents.openDevTools());
                window.loadURL(url);
                break;
        }
    }

}