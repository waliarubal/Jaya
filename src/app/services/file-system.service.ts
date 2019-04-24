import { Injectable } from '@angular/core';
import { IpcBaseService } from '@shared/ipc-base.service';
import { MessageModel, MessageType, DirectoryModel, Helpers } from '@common/index';

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
                let dirs = Helpers.Deserialize<DirectoryModel>(message.Data);
                console.log(dirs);
                break;
        }
    }

    GetDirectories(path: string): void {
        this.Send(MessageType.Directoties, 'e://');
    }

}