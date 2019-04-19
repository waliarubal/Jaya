import { app, BrowserWindow } from 'electron';
import * as Path from 'path';
import * as Url from 'url';
import { IpcService } from '@services/ipc.service';
import { Constants } from '@shared/constants';

let _window: BrowserWindow;
let _ipc: IpcService;

function CreateWindow(): void {
    if (_window)
        return;

    const path = Path.join(__dirname, 'jaya/index.html');
    const url = Url.format({
        pathname: path,
        protocol: 'file:',
        slashes: true
    });

    _window = new BrowserWindow({
        width: 800,
        height: 600, 
        webPreferences: {
            nodeIntegration: false,
            contextIsolation: true
        }
    });
    _window.loadURL(url);
    _window.webContents.openDevTools();
    _window.on('closed', DestroyWindow);

    _ipc = new IpcService(_window);
    _ipc.Event.on(Constants.IPC_CHANNEL, () => {});
}

function DestroyWindow(): void {
    if (_window) {
        _ipc.Stop();
        _window = null;
    }
        
    if (process.platform !== 'darwin')
        app.quit();
}

app.on('ready', CreateWindow);
app.on('activate', CreateWindow);
app.on('window-all-closed', DestroyWindow);