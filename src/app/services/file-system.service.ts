import { Injectable } from '@angular/core';
import { deserialize } from 'serializr';
import { IpcBaseService } from '@shared/ipc-base.service';
import { MessageModel, MessageType, DirectoryModel } from '@common/index';

@Injectable()
export class FileSystemService extends IpcBaseService {

    constructor() {
        super();

        this.Receive.subscribe((message: MessageModel) => this.OnMessageReceived(message));
    }

    protected Dispose(): void {
        this.Receive.unsubscribe();
    }

    private OnMessageReceived(message: MessageModel): void {
        switch (message.Type) {
            case MessageType.Directoties:
                let dir = deserialize(DirectoryModel, message.DataJson);
                dir.Directories.forEach(dir => console.log(dir.Name));
                break;
        }
    }

    async GetDirectories(path: string): Promise<DirectoryModel> {
        let response = await this.SendAsync(MessageType.Directoties, 'd://');
        let directory = deserialize(DirectoryModel, response.DataJson);
        return directory;
    }

}