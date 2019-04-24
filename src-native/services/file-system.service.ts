import * as Fs from 'fs';
import { IpcService } from './ipc.service';
import { BaseService, Constants, MessageModel, MessageType, DirectoryModel, Helpers } from '../../src-common';

export class FileSystemService extends BaseService {
    private readonly _ipc: IpcService;

    constructor(ipc: IpcService) {
        super();
        this._ipc = ipc;
        this._ipc.Receive.on(Constants.IPC_CHANNEL, (message: MessageModel) => this.OnMessage(message));
    }

    protected Dispose(): void {
        this._ipc.Receive.removeAllListeners(Constants.IPC_CHANNEL);
    }

    private OnMessage(message: MessageModel): void {
        switch (message.Type) {
            case MessageType.Directoties:
                Fs.readdir(message.Data, (ex: NodeJS.ErrnoException, files: string[]) => {
                    let dirs: DirectoryModel[] = [];
                    files.forEach(file => {
                        let dir = new DirectoryModel();
                        dir.Name = file;
                        dirs.push(dir);
                    })
                    message.Data = Helpers.Serialize(dirs);
                    this._ipc.Send(message);
                });
                break;
        }
    }
}