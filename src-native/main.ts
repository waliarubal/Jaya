import { app, BrowserWindow } from 'electron';
import * as Path from 'path';
import * as Url from 'url';
import { IpcService, FileSystemService } from './services';
import { BaseService } from '../src-common';

let _window: BrowserWindow;
let _services: BaseService[];

function CreateWindow(): void {
    if (_window)
        return;

    const path = Path.join(__dirname, '../../dist/jaya/index.html');
    const url = Url.format({
        pathname: path,
        protocol: 'file:',
        slashes: true
    });

    _window = new BrowserWindow({
        width: 800,
        height: 600,
        webPreferences: {
            nodeIntegration: true,
            contextIsolation: false
        }
    });

    _services = CreateServices(_window);

    _window.loadURL(url);
    _window.webContents.openDevTools();
    _window.on('closed', DestroyWindow);
}

function DestroyWindow(): void {
    if (_window) {
        _window = null;
    }

    if (process.platform !== 'darwin')
        app.quit();
}

function CreateServices(window: BrowserWindow): BaseService[] {
    let ipc = new IpcService(window);
    let services: BaseService[] = [
        ipc,
        new FileSystemService(ipc)
    ];
    return services;
}

function DestroyServices(services: BaseService[]): void {

}

app.on('ready', CreateWindow);
app.on('activate', CreateWindow);
app.on('window-all-closed', DestroyWindow);