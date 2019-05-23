import { OnInit, OnDestroy, HostListener } from '@angular/core';
import { BaseModel } from '@common/index';

export abstract class BaseComponent extends BaseModel implements OnInit, OnDestroy {

    constructor() {
       super();
    }

    async ngOnInit(): Promise<void> {
        await this.Initialize();
    }

    @HostListener('window:beforeunload')
    async ngOnDestroy(): Promise<void> {
        await this.Destroy();
        super.Clear();
    }

    protected async abstract Initialize(): Promise<void>;

    protected async abstract Destroy(): Promise<void>;

}