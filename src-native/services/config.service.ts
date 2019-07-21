import * as Path from 'path';
import { Constants, MessageModel, ConfigModel, MessageType, Helpers, AccountModel } from '../../src-common';
import { IpcService } from './ipc.service';
import { SuperService, SQLite, ElectronHelpers } from '../shared';

export class ConfigService extends SuperService {
    private readonly _dbDirectory: string;
    private _db: SQLite;

    constructor(private readonly _ipc: IpcService) {
        super();
        this._db = new SQLite();
        this._ipc.Receive.on(Constants.IPC_CHANNEL, (message: MessageModel) => this.OnMessage(message));

        this._dbDirectory = ElectronHelpers.GetUserDataPath();

        this.LoadConfigurations().then().catch(ex => console.log(ex));
    }

    get DbFilesDirectory(): string {
        return this._dbDirectory;
    }

    private async LoadConfigurations(): Promise<void> {
        const configFile = Path.join(this.DbFilesDirectory, 'config.sqlite');
        console.log('Config File Path: %s', configFile); 
        await this._db.Open(configFile);
    }

    protected async Dispose(): Promise<void> {
        this._ipc.Receive.removeAllListeners(Constants.IPC_CHANNEL);

        await this._db.Close();
        this._db = undefined;
    }

    private OnMessage(message: MessageModel): void {
        let config: ConfigModel;
        switch (message.Type) {
            case MessageType.GetConfig:
                config = Helpers.Deserialize<ConfigModel>(message.DataJson, ConfigModel);
                let value = config.Value;

                this._configDb.Find({ _key: config.Key }).then((configs: ConfigModel[]) => {
                    if (configs && configs.length === 1) {
                        value = configs[0].Value;
                        console.log(value);
                    }
                        
                    config.Value = value;
                    message.DataJson = Helpers.Serialize<ConfigModel>(config);
                    this._ipc.Send(message);
                }).catch((err: Error) => console.log(err));
                break;

            case MessageType.SetConfig:
                config = Helpers.Deserialize<ConfigModel>(message.DataJson, ConfigModel);
                this._configDb.Update({ _key: config.Key }, config).finally(() => {
                    this._ipc.Send(message);
                }).catch((err: Error) => console.log(err));
                break;

            case MessageType.DeleteConfig:
                let command = parseInt(message.DataJson);
                this._configDb.Delete({ _key: command }).then((count: number) => {

                }).catch((err: Error) => console.log(err));
                break;
        }
    }
}