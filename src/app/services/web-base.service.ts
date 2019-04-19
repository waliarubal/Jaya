import { IpcRenderer } from 'electron';
import { BaseService } from '@shared/base.service';

export abstract class WebBaseService extends BaseService {
    private readonly _ipc: IpcRenderer;

    constructor() {
        super();
        
        if ((<any>window).require) {
            try {
                this._ipc = (<any>window).require('electron').ipcRenderer;
            } catch (e) {
                throw e;
            }
        } else
            console.warn('Could not load electron IPC.');
    }

    protected get Ipc(): IpcRenderer {
        return this._ipc;
    }

}