import { Injectable, EventEmitter, Type } from '@angular/core';
import { WindowModel } from '@shared/window.model';
import { SuperService } from '@shared/super.service';
import { isNullOrUndefined } from 'util';

@Injectable({
    providedIn: 'root'
})
export class PopupService extends SuperService {
    private readonly _onOpen: EventEmitter<WindowModel>;
    private readonly _onClose: EventEmitter<void>;

    constructor() {
        super();
        this._onOpen = new EventEmitter();
        this._onClose = new EventEmitter();
    }

    get OnOpen(): EventEmitter<WindowModel> {
        return this._onOpen;
    }

    get OnClose(): EventEmitter<void> {
        return this._onClose;
    }

    get Data(): WindowModel {
        return this.Get('pd');
    }

    get IsOpen(): boolean {
        return !isNullOrUndefined(this.Data);
    }

    protected Dispose(): void {
        this._onOpen.unsubscribe();
        this._onClose.unsubscribe();
    }

    Open(title: string, component: Type<any>, isModal: boolean = true): void {
        let data = WindowModel.Empty();
        data.Title = title;
        data.IsModal = isModal;
        data.Component = component;
        this.Set('pd', data);
        this._onOpen.emit(this.Data);
    }

    Close(): void {
        this.Set('pd', undefined);
        this._onClose.emit();
    }
}