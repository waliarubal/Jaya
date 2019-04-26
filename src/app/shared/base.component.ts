import { OnInit, OnDestroy } from '@angular/core';
import { BaseModel } from '@common/index';

export abstract class BaseComponent extends BaseModel implements OnInit, OnDestroy {

    constructor() {
       super();
    }

    ngOnInit(): void {
        this.Initialize();
    }

    ngOnDestroy(): void {
        this.Destroy();
        super.Clear();
    }

    protected abstract Initialize(): void;

    protected abstract Destroy(): void;

}