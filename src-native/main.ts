import { app, BrowserWindow } from 'electron';
import * as Path from 'path';
import * as Url from 'url';

let _window: BrowserWindow;

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
}

function DestroyWindow(): void {
    if (_window) {
        _window = null;
    }
        
    if (process.platform !== 'darwin')
        app.quit();
}

app.on('ready', CreateWindow);
app.on('activate', CreateWindow);
app.on('window-all-closed', DestroyWindow);