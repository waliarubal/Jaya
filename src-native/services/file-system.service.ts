import { IpcService } from './ipc.service';
import { BaseService, Constants, MessageModel, MessageType } from '../../src-common';

export class FileSystemService extends BaseService {
    private readonly _ipc: IpcService;

    constructor(ipc: IpcService){
        super();
        this._ipc = ipc;
        this._ipc.Receive.on(Constants.IPC_CHANNEL, this.OnMessage);
    }

    protected Dispose(): void {
        this._ipc.Receive.removeListener(Constants.IPC_CHANNEL, this.OnMessage);
    }

    private OnMessage(message: MessageModel): void {
        switch(message.Type) {
            case MessageType.Handshake:
                console.log('IPC bridge created.');
                break;
        }
    }
}