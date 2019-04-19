import { ElectronService } from 'ngx-electron';
import { MessageModel } from '@shared/models/message.model';

export abstract class BaseComponent {
    private readonly _electron: ElectronService;
    
    constructor(electron: ElectronService) {
        this._electron = electron;
        this._electron.ipcRenderer.on('message', (event: any, argument: MessageModel) => this.OnMessage(argument));
    }

    protected abstract OnMessage(message: MessageModel): void;

    protected SendMessage(message: MessageModel) {
        this._electron.ipcRenderer.send('message', message);
    }

}