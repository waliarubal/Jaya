import {app} from 'electron';
import { SuperService } from '../shared/super.service';
import { IpcService } from './ipc.service';
import { MessageModel, Constants, MessageType, Helpers, CommandModel, CommandType } from '../../src-common';

export class CommandService extends SuperService {

    constructor(private readonly _ipc: IpcService){
        super();
        this._ipc.Receive.on(Constants.IPC_CHANNEL, (message: MessageModel) => this.OnMessage(message));
    }

    protected async Dispose(): Promise<void> {
        this._ipc.Receive.removeAllListeners(Constants.IPC_CHANNEL);
    }

    private OnMessage(message: MessageModel): void {
        if (message.Type !== MessageType.Command)
            return;

        const command = Helpers.Deserialize<CommandModel>(message.DataJson, CommandModel);
        switch(command.Command) {
            case CommandType.Exit:
                app.quit();
                break;
        }
    }
    
}