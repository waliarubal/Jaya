import { BrowserWindow, remote, app } from 'electron';
import * as Url from 'url';
import * as Fs from 'fs';
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

    static async FileExists(fileName: string): Promise<boolean> {
        return new Promise<boolean>((resolve, reject) => {
            try {
                Fs.exists(fileName, (exists: boolean) => resolve(exists));
            } catch (ex) {
                reject(ex);
            }
        });
    }

    static ReadFileBufferAsync(fileName: string): Promise<Buffer> {
        return new Promise<Buffer>((resolve, reject) => {
            Fs.readFile(fileName, (error: NodeJS.ErrnoException, data: Buffer) => {
                if (error)
                    reject(error);
                else
                    resolve(data);
            });
        })
    }

    static async ReadFileAsync(fileName: string): Promise<string> {
        return new Promise<string>((resolve, reject) => {
            Fs.readFile(fileName, 'utf8', (error: NodeJS.ErrnoException, data: string) => {
                if (error)
                    reject(error);
                else
                    resolve(data);
            });
        });
    }

    static async WriteFileAsync(fileName: string, data: string | Buffer): Promise<void> {
        return new Promise<void>((resolve, reject) => {
            Fs.writeFile(fileName, data, (error: NodeJS.ErrnoException) => {
                if (error)
                    reject(error);
                else
                    resolve();
            });
        });
    }

    static GetUserDataPath(): string {
        return (app || remote.app).getPath('userData');
    }

}