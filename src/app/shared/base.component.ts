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

    ngOnInit(): void {
        this.Initialize()
            .then()
            .catch((ex) => console.log(ex));
    }

    @HostListener('window:beforeunload')
    ngOnDestroy(): void {
        this._onConfigChangedSubscription.unsubscribe();
        this.Destroy()
            .then(() => super.Clear())
            .catch((ex) => console.log(ex));
    }

    protected abstract OnConfigChanged(config: ConfigModel): void;

    protected async abstract Initialize(): Promise<void>;

    protected async abstract Destroy(): Promise<void>;

}