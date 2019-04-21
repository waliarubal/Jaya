import { IpcService } from './ipc.service';
import { BrowserWindow } from 'electron';
import { MessageModel } from '@shared/models/message.model';

export class FileSystemService extends IpcService {

    constructor(window: BrowserWindow) {
        super(window);
    }

    protected Receive(message: MessageModel) {
        
    }

}