import { WebBaseService } from '@services/web-base.service';
import { MessageModel } from '@shared/models';
import { Constants } from '@shared/constants';
import { Injectable, EventEmitter } from '@angular/core';

@Injectable()
export class IpcService extends WebBaseService {
    private readonly _event: EventEmitter<MessageModel>;

    constructor() {
        super();

        this._event = new EventEmitter();
        if (this.Ipc)
            this.Ipc.on(Constants.IPC_CHANNEL, (event: any, argument: any) => this.Receive.emit(argument));
    }

    get Receive(): EventEmitter<MessageModel> {
        return this._event;
    }

    Send(message: MessageModel): void {
        if (this.Ipc)
            this.Ipc.send(Constants.IPC_CHANNEL, message);
        else
            console.error("Couldn't send message as electron IPC is not loaded.");
    }

    protected Dispose(): void {
        this._event.isStopped = true;
        if (this.Ipc)
            this.Ipc.removeAllListeners(Constants.IPC_CHANNEL);
    }

}