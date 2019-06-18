import { app, BrowserWindow } from 'electron';
import * as Path from 'path';
import * as Url from 'url';
import { IpcService, ConfigService, CommandService } from './services';
import { FileSystemService, DropboxService } from './services/providers'
import { SuperService } from './shared';

class Main {
    private _window: BrowserWindow;
    private _services: SuperService[];

    Initialize(): void {
        app.on('ready', () => this.CreateWindow());
        app.on('activate', () => this.CreateWindow());
        app.on('window-all-closed', async () => await this.DestroyWindow());
        app.on('browser-window-created', (event: Electron.Event, window: BrowserWindow) => window.setMenu(null));
    }

    get MainWindow(): BrowserWindow {
        return this._window;
    }

    private CreateServices(window: BrowserWindow): SuperService[] {
        let ipc = new IpcService(window);
        return [
            ipc,
            new ConfigService(ipc),
            new CommandService(ipc),
            new FileSystemService(ipc),
            new DropboxService(ipc)
        ];
    }

    private async DestroyServices(services: SuperService[]): Promise<void> {
        while (services.length > 0) {
            let service = services.pop();
            await service.Stop();
            service = null;
        }
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
            minHeight: 600,
            minWidth: 800,
            webPreferences: {
                nodeIntegration: true,
                contextIsolation: false
            }
        });

        this._services = this.CreateServices(window);

        window.loadURL(url).then(() => window.webContents.openDevTools());
        window.on('closed', async () => await this.DestroyWindow());
    }

    private async DestroyWindow(): Promise<void> {
        if (this._services)
            await this.DestroyServices(this._services);

        if (this._window)
            this._window = null;

        if (process.platform !== 'darwin')
            app.quit();
    }

}

let _main = new Main();
_main.Initialize();