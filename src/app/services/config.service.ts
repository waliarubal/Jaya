import { Injectable, EventEmitter } from '@angular/core';
import { SuperService } from '@shared/super.service';
import { IpcService } from '@services/ipc.service';
import { MessageType, ConfigModel, Helpers } from '@common/index';

@Injectable({
    providedIn: 'root'
})
export class ConfigService extends SuperService {
    private readonly _onConfigChanged: EventEmitter<ConfigModel>;

    constructor(private readonly _ipc: IpcService) {
        super();
        this._onConfigChanged = new EventEmitter();
    }

    get OnConfigChanged(): EventEmitter<ConfigModel> {
        return this._onConfigChanged;
    }

    protected Dispose(): void {
        this._ipc.Receive.unsubscribe();
        this.OnConfigChanged.isStopped = true;
    }

    async SetValue<T>(key: number, value: T): Promise<void> {
        let config = new ConfigModel(key, value);
        const configJson = Helpers.Serialize<ConfigModel>(config);
        let response = await this._ipc.SendAsync(MessageType.SetConfig, configJson);
        config = Helpers.Deserialize<ConfigModel>(response.DataJson, ConfigModel);
        if (config && this.OnConfigChanged)
            this.OnConfigChanged.emit(config);
    }

    async GetValue<T>(key: number, value: T): Promise<T> {
        let config = new ConfigModel(key, value)
        const configJson = Helpers.Serialize<ConfigModel>(config);
        const response = await this._ipc.SendAsync(MessageType.GetConfig, configJson);
        config = Helpers.Deserialize<ConfigModel>(response.DataJson, ConfigModel);
        return <T>config.Value;
    }

    DeleteConfiguration(key: string): void {
        this._ipc.Send(MessageType.DeleteConfig, key);
    }

}