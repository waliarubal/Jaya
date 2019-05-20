export class Constants {
    static readonly IPC_CHANNEL: string = 'ipc_channel';
    static readonly SEPARATOR: string = '¶';
}

export enum Commands {
    Cut = 'Cut',
    Copy = 'Copy',
    Paste = 'Paste',
    MoveTo = 'Move to...',
    CopyTo = 'Copy to...',
    Delete = 'Delete',
    Rename = 'Rename',
    SelectAll = 'Select all',
    SelectNone = 'Select none',
    NewFolder = 'New folder',
    Properties = 'Properties',
    Open = 'Open',
    Exit = 'Exit'
}