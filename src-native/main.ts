import { app, BrowserWindow } from 'electron';
import * as Path from 'path';
import * as Url from 'url';
import { IpcService, FileSystemService } from './services';
import { BaseService } from '../src-common';

class Main {
    private _window: BrowserWindow;
    private _services: BaseService[];

    Initialize(): void {
        app.on('ready',() => this.CreateWindow());
        app.on('activate', () => this.CreateWindow());
        app.on('window-all-closed', () => this.DestroyWindow());
        app.on('browser-window-created', (event: Electron.Event, window: BrowserWindow) => window.setMenu(null));
    }

    private CreateServices(window: BrowserWindow): BaseService[] {
        let ipc = new IpcService(window);
        let services: BaseService[] = [
            ipc,
            new FileSystemService(ipc)
        ];
        return services;
    }

    private DestroyServices(services: BaseService[]): void {

    }

    private CreateWindow(): void {
        if (this._window)
            return;

        const path = Path.join(__dirname, '../../dist/jaya/index.html');
        const url = Url.format({
            pathname: path,
            protocol: 'file:',
            slashes: true
        });

        let window = this._window = new BrowserWindow({
            width: 800,
            height: 600,
            webPreferences: {
                nodeIntegration: true,
                contextIsolation: false
            }
        });

        this._services = this.CreateServices(window);

        window.loadURL(url);
        window.webContents.openDevTools();
        window.on('closed', () => this.DestroyWindow());
    }

    private DestroyWindow(): void {
        if (this._services)
            this.DestroyServices(this._services);

        if (this._window)
            this._window = null;

        if (process.platform !== 'darwin')
            app.quit();
    }

}

let _main = new Main();
_main.Initialize();