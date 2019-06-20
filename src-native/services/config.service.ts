import { app, remote } from 'electron';
import * as Path from 'path';
import { Constants, MessageModel, ConfigModel, MessageType, Helpers, AccountModel } from '../../src-common';
import { IpcService } from './ipc.service';
import { SuperService, DataBase } from '../shared';

export class ConfigService extends SuperService {
    private readonly _dbDirectory: string;
    private _configDb: DataBase<ConfigModel>;
    private _accountsDb: DataBase<AccountModel>;

    constructor(private readonly _ipc: IpcService) {
        super();
        this._ipc.Receive.on(Constants.IPC_CHANNEL, (message: MessageModel) => this.OnMessage(message));

        const userDataPath = (app || remote.app).getPath('userData');
        this._dbDirectory = userDataPath;
        console.log('DB Files Directory: %s', this.DbFilesDirectory);

        this.LoadConfigurations();
    }

    get DbFilesDirectory(): string {
        return this._dbDirectory;
    }

    private LoadConfigurations(): void {
        this._configDb = new DataBase();
        this._configDb.Open(Path.join(this.DbFilesDirectory, 'config.db'));

        this._accountsDb = new DataBase();
        this._accountsDb.Open(Path.join(this.DbFilesDirectory, 'accounts.db'));
    }

    protected async Dispose(): Promise<void> {
        this._ipc.Receive.removeAllListeners(Constants.IPC_CHANNEL);

        this._configDb.Close();
        this._accountsDb.Close();
    }

    private OnMessage(message: MessageModel): void {
        let config: ConfigModel;
        switch (message.Type) {
            case MessageType.GetConfig:
                config = Helpers.Deserialize<ConfigModel>(message.DataJson, ConfigModel);
                let value = config.Value;

                this._configDb.Find({ _key: config.Key }).then((configs: ConfigModel[]) => {
                    if (configs && configs.length === 1)
                        value = configs[0].Value;

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