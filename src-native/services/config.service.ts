import { app } from 'electron';
import { BaseService, Constants, MessageModel, MessageType, Helpers, ConfigModel } from '../../src-common';
import { IpcService } from './ipc.service';

export class ConfigService extends BaseService {

    constructor(private readonly _ipc: IpcService) {
        super();
        this._ipc.Receive.on(Constants.IPC_CHANNEL, (message: MessageModel) => this.OnMessage(message));
        app.getPath('config.json');
        app.setPath('config.json', )
    }

    protected Dispose(): void {
        this._ipc.Receive.removeAllListeners(Constants.IPC_CHANNEL);
    }

    private OnMessage(message: MessageModel): void {
        switch (message.Type) {
            case MessageType.GetSetConfig:
                const config = Helpers.Deserialize<ConfigModel>(message.DataJson, ConfigModel);
                let value = config.Value;W

                if (this._store.has(config.Key)) {
                    value = <string>this._store.get(config.Key);

                    this._store.set(config.Key, config.Value);

                    config.Value = value;
                }
                else
                    this._store.set(config.Key, value);

                message.DataJson = Helpers.Serialize<ConfigModel>(config);
                this._ipc.Send(message);
                break;

            case MessageType.DeleteConfig:
                if (this._store.has(message.DataJson))
                    this._store.delete(message.DataJson);
                break;
        }
    }
}