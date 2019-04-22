import { MessageModel } from '@shared/models/message.model';
import { BaseModel } from '@shared/base.model';
import { IpcService } from '@services/ipc.service';

export abstract class BaseComponent extends BaseModel {
    private readonly _ipc: IpcService;

    constructor(ipc: IpcService) {
       super();
       this._ipc = ipc;
       ipc.Receive.subscribe((message: MessageModel) => this.ReceiveMessage(message));
    }

    private get Ipc(): IpcService {
        return this._ipc;
    }

    protected SendMessage(message: MessageModel) {
        this.Ipc.Send(message);
    }

    protected abstract ReceiveMessage(message: MessageModel): void;

}