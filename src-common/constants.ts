export enum Commands {
    File,
    Edit,
    View,
    Help,
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

export interface ICommand {
    readonly Command?: Commands;
    readonly Title?: string;
    readonly IconClass?: string;
    readonly IsSeparator?: boolean;
    readonly Commands?: ICommand[];
}

export class Constants {
    static readonly IPC_CHANNEL: string = 'ipc_channel';
    static readonly SEPARATOR: string = '¶';

    static readonly MENU_DATA: ICommand[] = [
        {
            Command: Commands.File,
            Title: 'File',
            Commands: [
                {
                    Command: Commands.NewFolder,
                    Title: 'New folder',
                    IconClass: 'fa fa-folder-plus',
                },
                { IsSeparator: true },
                {
                    Command: Commands.Properties,
                    Title: 'Properties',
                    IconClass: 'fa fa-info'
                },
                {
                    Command: Commands.Open,
                    Title: 'Open',
                    IconClass: 'fa fa-edit'
                },
                { IsSeparator: true },
                {
                    Command: Commands.Exit,
                    Title: 'Exit',
                    IconClass: 'fa fa-sign-out-alt'
                }
            ]
        },
    ];
}