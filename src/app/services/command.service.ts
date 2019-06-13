import { SuperService } from '@shared/super.service';
import { IpcService } from '@services/ipc.service';
import { Injectable } from '@angular/core';
import { CommandType, CommandModel, MessageType, Helpers } from '@common/index';

@Injectable({
    providedIn: 'root'
})
export class CommandService extends SuperService {

    constructor(private readonly _ipc: IpcService) {
        super();
    }

    protected Dispose(): void {
        this._ipc.Receive.unsubscribe();
    }

    Execute(command: CommandType, parameter: any): void {
        let commandPacket = new CommandModel(command, parameter);
        this._ipc.Send(MessageType.Command, Helpers.Serialize<CommandModel>(commandPacket));
    }

}