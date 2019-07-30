import * as Path from 'path';
import { Constants, MessageModel, ConfigModel, MessageType, Helpers, AccountModel, Dictionary } from '../../src-common';
import { IpcService } from './ipc.service';
import { SuperService, ElectronHelpers } from '../shared';

export class ConfigService extends SuperService {
    private readonly RECORD_SEPARATOR = '\n';
    private readonly DATA_SEPARATOR = '\u2665';

    private readonly _configFile: string;
    private readonly _configs: Dictionary<number, any>;

    constructor(private readonly _ipc: IpcService) {
        super();
        this._configs = new Dictionary();
        
        this._configFile = Path.join(ElectronHelpers.GetUserDataPath(), 'config.dat');
        console.log('Config File: %s', this._configFile);
    }

    protected async Initialize(): Promise<void> {
        this._ipc.Receive.on(Constants.IPC_CHANNEL, (message: MessageModel) => this.OnMessage(message));

        if (!ElectronHelpers.FileExists(this._configFile))
            return;

        const data = await ElectronHelpers.ReadFileAsync(this._configFile);
        if (!data)
            return;

        let lines = data.split(this.RECORD_SEPARATOR);
        for (let line of lines) {
            let parts = line.split(this.DATA_SEPARATOR);
            if (!parts || parts.length !== 2)
                continue;

            this._configs.Set(parseInt(parts[0]), parts[1]);
        }
    }

    protected async Dispose(): Promise<void> {
        this._ipc.Receive.removeAllListeners(Constants.IPC_CHANNEL);

        let data = '';
        for(let key of this._configs.Keys)
            data+= `${key}${this.DATA_SEPARATOR}${this._configs.Get(key)}${this.RECORD_SEPARATOR}`;

        await ElectronHelpers.WriteFileAsync(this._configFile, data);
    }

    private OnMessage(message: MessageModel): void {
        let config: ConfigModel;
        switch (message.Type) {
            case MessageType.GetConfig:
                config = Helpers.Deserialize<ConfigModel>(message.DataJson, ConfigModel);
                let value = config.Value;

                if (this._configs.IsHaving(config.Key))
                    value = this._configs.Get(config.Key);

                config.Value = value;
                message.DataJson = Helpers.Serialize<ConfigModel>(config);
                this._ipc.Send(message);
                break;

            case MessageType.SetConfig:
                config = Helpers.Deserialize<ConfigModel>(message.DataJson, ConfigModel);
                this._configs.Set(config.Key, config.Value);
                this._ipc.Send(message);
                break;

            case MessageType.DeleteConfig:
                let command = parseInt(message.DataJson);
                this._configs.Delete(command);
                break;
        }
    }
}