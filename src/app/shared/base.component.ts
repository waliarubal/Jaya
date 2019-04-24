import { BaseModel, MessageModel } from '@common/index';
import { IpcService } from '@services/ipc.service';

export abstract class BaseComponent extends BaseModel {

    constructor(private _ipc: IpcService) {
       super();
       _ipc.Receive.subscribe((message: MessageModel) => this.ReceiveMessage(message));
    }

    protected SendMessage(message: MessageModel) {
        this._ipc.Send(message);
    }

    protected abstract ReceiveMessage(message: MessageModel): void;

}