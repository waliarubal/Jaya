import * as Fs from 'fs';
import { serialize } from 'serializr';
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
                Fs.readdir(message.DataJson, (ex: NodeJS.ErrnoException, files: string[]) => {
                    let directory = new DirectoryModel();
                    directory.Name = message.DataJson;
                    directory.Directories = [];
                    files.forEach(file => {
                        let dir = new DirectoryModel();
                        dir.Name = file;
                        directory.Directories.push(dir);
                    })
                    message.DataJson = serialize<DirectoryModel>(directory);
                    this._ipc.Send(message);
                });
                break;
        }
    }
}