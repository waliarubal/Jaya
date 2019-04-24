import { IpcRenderer } from 'electron';
import { OnDestroy } from '@angular/core';
import { BaseService } from '@common/index';

export abstract class WebBaseService extends BaseService implements OnDestroy {
    private readonly _ipc: IpcRenderer;

    constructor() {
        super();
        
        if ((<any>window).require) {
            try {
                this._ipc = (<any>window).require('electron').ipcRenderer;
            } catch (e) {
                console.error(e);
            }
        } else
            console.error('Could not load electron IPC.');
    }

    protected get Ipc(): IpcRenderer {
        return this._ipc;
    }

    ngOnDestroy(): void {
        this.Dispose();
    }

}