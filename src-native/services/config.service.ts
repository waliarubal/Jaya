import { app, remote } from 'electron';
import * as Path from 'path';
import * as Fs from 'fs';
import { Constants, MessageModel, ConfigModel, MessageType, Helpers, IClonable, Dictionary } from '../../src-common';
import { IpcService } from './ipc.service';
import { SuperService } from '../shared';

class ConfigCollection implements IClonable {
    private _configs: ConfigModel[];

    constructor() {
        this._configs = [];
    }

    get Configs(): ConfigModel[] {
        return this._configs;
    }

    Clone(object: any): void {
        if (object._configs) {
            this._configs.length = 0;
            for (let configObj of object._configs) {
                let config = new ConfigModel();
                config.Clone(configObj);
                this._configs.push(config);
            }
        }
    }

}

export class ConfigService extends SuperService {
    private readonly _configFile: string;
    private _configs: Dictionary<number, any>;

    constructor(private readonly _ipc: IpcService) {
        super();
        this._ipc.Receive.on(Constants.IPC_CHANNEL, (message: MessageModel) => this.OnMessage(message));

        const userDataPath = (app || remote.app).getPath('userData');
        this._configFile = Path.join(userDataPath, 'config.json');
        console.log('Config File: %s', this.ConfigFileName);
    }

    get ConfigFileName(): string {
        return this._configFile;
    }

    private async ReadFileAsync(fileName: string): Promise<string> {
        return new Promise<string>((resolve, reject) => {
            Fs.readFile(fileName, 'utf8', (error: NodeJS.ErrnoException, data: string) => {
                if (error)
                    reject(error);
                else
                    resolve(data);
            });
        });
    }

    private async WriteFileAsync(fileName: string, data: string): Promise<void> {
        return new Promise<void>((resolve, reject) => {
            Fs.writeFile(fileName, data, (error: NodeJS.ErrnoException) => {
                if (error)
                    reject(error);
                else
                    resolve();
            });
        });
    }

    private async ReadConfigFile(fileName: string): Promise<void> {
        if (this._configs)
            return;

        let json: string;
        try {
            json = await this.ReadFileAsync(fileName);
        } catch (ex) {
            console.error(ex);
        }

        this._configs = new Dictionary();
        if (!json)
            return;

        var configs = Helpers.Deserialize<ConfigCollection>(json, ConfigCollection);
        for (let config of configs.Configs)
            this._configs.Set(config.Key, config.Value);
    }

    private async WriteConfigFile(fileName: string): Promise<void> {
        if (!this._configs)
            return;

        let collection = new ConfigCollection();
        for (let key of this._configs.Keys) {
            let value = this._configs.Get(key);
            collection.Configs.push(new ConfigModel(key, value));
        }
        let json = Helpers.Serialize<ConfigCollection>(collection);
        await this.WriteFileAsync(fileName, json);
    }

    protected async Dispose(): Promise<void> {
        this._ipc.Receive.removeAllListeners(Constants.IPC_CHANNEL);
        await this.WriteConfigFile(this.ConfigFileName);
    }

    private OnMessage(message: MessageModel): void {
        let config: ConfigModel;
        switch (message.Type) {
            case MessageType.GetConfig:
                config = Helpers.Deserialize<ConfigModel>(message.DataJson, ConfigModel);
                let value = config.Value;
                this.ReadConfigFile(this.ConfigFileName).then(() => {
                    if (this._configs.IsHaving(config.Key))
                        value = this._configs.Get(config.Key);

                    config.Value = value;
                    message.DataJson = Helpers.Serialize<ConfigModel>(config);
                    this._ipc.Send(message);
                });
                break;

            case MessageType.SetConfig:
                config = Helpers.Deserialize<ConfigModel>(message.DataJson, ConfigModel);
                this.ReadConfigFile(this.ConfigFileName).then(() => {
                    this._configs.Set(config.Key, config.Value);
                    this._ipc.Send(message);
                })
                break;

            case MessageType.DeleteConfig:
                let command = parseInt(message.DataJson);
                this.ReadConfigFile(this.ConfigFileName).then(() => {
                    if (this._configs.IsHaving(command))
                        this._configs.Delete(command);
                });
                break;
        }
    }
}