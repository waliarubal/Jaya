import { BrowserWindow, BrowserView } from 'electron';
import * as Url from 'url';

export class ElectronHelpers {

    static async OpenModal(url: string, closeOnUrl: string, parent: BrowserWindow): Promise<BrowserWindow> {
        url = Url.format(url);
        let window = new BrowserWindow({
            modal: true,
            parent: parent,
            maximizable: false,
            focusable: true,
            skipTaskbar: true,
            useContentSize: true,
            title: 'Loading...',
            webPreferences: {
                contextIsolation: true,
                nodeIntegration: false
            }
        });
        await window.loadURL(url);
        window.webContents.on('dom-ready', (event: Electron.Event) => {
            console.log(event.sender);
        });
        return window;
    }

}