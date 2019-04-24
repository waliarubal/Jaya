import { BrowserWindow } from 'electron';
import { IpcService } from './ipc.service';
import { MessageModel, MessageType } from '../../src-common';

export class FileSystemService extends IpcService {

    constructor(window: BrowserWindow) {
        super(window);
    }

    protected Receive(message: MessageModel): void {
        switch (message.Type) {
            case MessageType.Handshake:
                console.log('IPC bridge created.');
                break;
        }
    }

}