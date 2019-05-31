import { OnInit, OnDestroy, HostListener } from '@angular/core';
import { BaseModel, ConfigModel } from '@common/index';
import { ConfigService } from '@services/config.service';
import { Subscription } from 'rxjs';

export abstract class BaseComponent extends BaseModel implements OnInit, OnDestroy {
    private readonly _onConfigChangedSubscription: Subscription;

    constructor(protected _config: ConfigService) {
        super();
        this._onConfigChangedSubscription = _config.OnConfigChanged.subscribe((config: ConfigModel) => this.OnConfigChanged(config));
    }

    async ngOnInit(): Promise<void> {
        await this.Initialize();
    }

    @HostListener('window:beforeunload')
    async ngOnDestroy(): Promise<void> {
        this._onConfigChangedSubscription.unsubscribe();
        await this.Destroy();
        super.Clear();
    }

    protected abstract OnConfigChanged(config: ConfigModel): void;

    protected async abstract Initialize(): Promise<void>;

    protected async abstract Destroy(): Promise<void>;

}