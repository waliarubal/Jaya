import { CommandType, IMenu } from './interfaces/IMenu';

export class Constants {
    static readonly IPC_CHANNEL: string = 'ipc_channel';
    static readonly SEPARATOR: string = '¶';

    static readonly MENU_DATA: IMenu[] = [
        {
            Command: CommandType.File,
            Label: 'File',
            SubMenus: [
                {
                    Command: CommandType.NewFolder,
                    Label: 'New folder',
                    IconClass: 'fa fa-folder-plus',
                },
                { IsSeparator: true },
                {
                    Command: CommandType.Properties,
                    Label: 'Properties',
                    IconClass: 'fa fa-info'
                },
                {
                    Command: CommandType.Open,
                    Label: 'Open',
                    IconClass: 'fa fa-edit'
                },
                { IsSeparator: true },
                {
                    Command: CommandType.Exit,
                    Label: 'Exit',
                    IconClass: 'fa fa-sign-out-alt'
                }
            ]
        },
        {
            Command: CommandType.Edit,
            Label: 'Edit',
            SubMenus: [
                {
                    Command: CommandType.Cut,
                    Label: 'Cut',
                    IconClass: 'fa fa-cut'
                },
                {
                    Command: CommandType.Copy,
                    Label: 'Copy',
                    IconClass: 'fa fa-copy'
                },
                {
                    Command: CommandType.Paste,
                    Label: 'Paste',
                    IconClass: 'fa fa-paste'
                },
                { IsSeparator: true },
                {
                    Command: CommandType.MoveTo,
                    Label: 'Move to...'
                },
                {
                    Command: CommandType.CopyTo,
                    Label: 'Copy to...'
                },
                { IsSeparator: true },
                {
                    Command: CommandType.Delete,
                    Label: 'Delete',
                    IconClass: 'fa fa-trash'
                },
                {
                    Command: CommandType.Rename,
                    Label: 'Rename',
                    IconClass: 'fa fa-signature'
                },
                { IsSeparator: true },
                {
                    Command: CommandType.SelectAll,
                    Label: 'Select all',
                    IconClass: 'fas fa-check-double'
                },
                {
                    Command: CommandType.SelectNone,
                    Label: 'Select none'
                }
            ]
        },
        {
            Command: CommandType.View,
            Label: 'View',
            SubMenus: [
                {
                    Command: CommandType.NavigationPane,
                    Label: 'Navigation pane',
                    IsCheckable: true
                },
                {
                    Command: CommandType.PreviewPane,
                    Label: 'Preview pane',
                    IsCheckable: true
                },
                {
                    Command: CommandType.DetailsPane,
                    Label: 'Details pane',
                    IsCheckable: true
                },
                { IsSeparator: true },
                {
                    Command: CommandType.ItemCheckBoxes,
                    Label: 'Item check boxes',
                    IsCheckable: true
                },
                {
                    Command: CommandType.FileNameExtensions,
                    Label: 'File name extensions',
                    IsCheckable: true
                },
                {
                    Command: CommandType.HiddenItems,
                    Label: 'Hidden items',
                    IsCheckable: true
                }
            ]
        },
        {
            Command: CommandType.Help,
            Label: 'Help',
            SubMenus: [
                {
                    Command: CommandType.License,
                    Label: 'View License',
                    IconClass: 'fa fa-certificate'
                },
                {
                    Command: CommandType.License,
                    Label: 'Privacy Statement',
                    IconClass: 'fa fa-user-secret'
                },
                { IsSeparator: true },
                {
                    Command: CommandType.Update,
                    Label: 'Check for Updates...',
                    IconClass: 'fa fa-retweet'
                },
                { IsSeparator: true },
                {
                    Command: CommandType.About,
                    Label: 'About',
                    IconClass: 'fa fa-info-circle'
                }
            ]
        }
    ];
}