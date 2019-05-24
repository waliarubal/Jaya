import { OnInit, OnDestroy, HostListener } from '@angular/core';
import { BaseModel, ConfigModel } from '@common/index';
import { ConfigService } from '@services/config.service';

export abstract class BaseComponent extends BaseModel implements OnInit, OnDestroy {

    constructor(protected _config: ConfigService) {
        super();
        _config.OnConfigChanged.subscribe((config: ConfigModel) => this.OnConfigChanged(config));
    }

    async ngOnInit(): Promise<void> {
        await this.Initialize();
    }

    @HostListener('window:beforeunload')
    async ngOnDestroy(): Promise<void> {
        this._config.OnConfigChanged.unsubscribe();
        await this.Destroy();
        super.Clear();
    }

    protected abstract OnConfigChanged(config: ConfigModel): void;

    protected async abstract Initialize(): Promise<void>;

    protected async abstract Destroy(): Promise<void>;

}