import { BrowserWindow } from 'electron';
import { IpcService } from './ipc.service';
import { MessageModel } from '../../shared';

export class FileSystemService extends IpcService {

    constructor(window: BrowserWindow) {
        super(window);
    }

    protected Receive(message: MessageModel) {
        console.log(message);
    }

}