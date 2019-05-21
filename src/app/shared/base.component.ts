import { OnInit, OnDestroy, HostListener } from '@angular/core';
import { BaseModel } from '@common/index';

export abstract class BaseComponent extends BaseModel implements OnInit, OnDestroy {

    constructor() {
       super();
    }

    ngOnInit(): void {
        this.Initialize();
    }

    @HostListener('window:beforeunload')
    async ngOnDestroy(): Promise<void> {
        await this.Destroy();
        super.Clear();
    }

    protected abstract Initialize(): void;

    protected async abstract Destroy(): Promise<void>;

}