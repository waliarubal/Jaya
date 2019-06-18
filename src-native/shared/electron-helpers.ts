import { BrowserWindow } from 'electron';
import * as Url from 'url';
import { Constants } from '../../src-common';

export class ElectronHelpers {

    static CreateWindow(url: string, parent: BrowserWindow = null, isModal: boolean = true): BrowserWindow {
        url = Url.format(url);
        let window = new BrowserWindow({
            modal: isModal,
            parent: parent,
            maximizable: false,
            focusable: true,
            skipTaskbar: true,
            useContentSize: true,
            title: 'Loading...',
            backgroundColor: Constants.BACK_COLOR,
            webPreferences: {
                contextIsolation: true,
                nodeIntegration: false
            }
        });
        return window;
    }

    static GetHistory(window: BrowserWindow): string[] {
        if (window.webContents)
            return (<any>window.webContents).history;

        return null;
    }

}