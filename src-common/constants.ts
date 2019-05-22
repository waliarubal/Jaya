export class Constants {
    static readonly IPC_CHANNEL: string = 'ipc_channel';
    static readonly SEPARATOR: string = '¶';
}

export enum Commands {
    Cut = 'Cut',
    Copy = 'Copy',
    Paste = 'Paste',
    CopyPath = 'Copy path',
    MoveTo = 'Move to...',
    CopyTo = 'Copy to...',
    Delete = 'Delete',
    Rename = 'Rename',
    SelectAll = 'Select all',
    SelectNone = 'Select none',
    NewFolder = 'New folder',
    Properties = 'Properties',
    Open = 'Open',
    Exit = 'Exit',
    NavigationPane = 'Navigation pane',
    PreviewPane = 'Preview pane',
    DetailsPane = 'Details pane',
    ItemCheckBoxes = 'Item check boxes',
    FileNameExtensions = 'File name extensions',
    HiddenItems = 'Hidden items',
    License = 'View License',
    PrivacyStatement = 'Privacy Statement',
    Update = 'Check for Updates...',
    About = 'About',
    PinToQuickAccess = 'Pin to Quick access'
}