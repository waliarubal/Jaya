export enum CommandType {
    File,
    Edit,
    View,
    Help,
    Cut,
    Copy,
    Paste,
    CopyPath,
    MoveTo,
    CopyTo,
    Delete,
    Rename,
    SelectAll,
    SelectNone,
    NewFolder,
    Properties,
    Open,
    Exit,
    NavigationPane,
    PreviewPane,
    DetailsPane,
    ItemCheckBoxes,
    FileNameExtensions,
    HiddenItems,
    License,
    PrivacyStatement,
    Update,
    About,
    Refresh,
    Providers,
    OpenWindow
}

export interface IMenu {
    readonly Command?: CommandType;
    readonly Label?: string;
    readonly IconClass?: string;
    readonly IsSeparator?: boolean;
    readonly SubMenus?: IMenu[];
    readonly IsCheckable?: boolean;
    IsChecked?: boolean;
}