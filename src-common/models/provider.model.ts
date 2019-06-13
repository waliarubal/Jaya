import { Helpers } from '../helpers';
import { DirectoryModel } from './directory.model';
import { IClonable } from '../interfaces/IClonable';

export enum ProviderType {
    FileSystem,
    Dropbox
}

export class ProviderModel implements IClonable {
    private _type: ProviderType;
    private _id: string;
    private _name: string;
    private _icon: string;
    private _directory: DirectoryModel;

    get Type(): ProviderType {
        return this._type;
    }

    get Id(): string {
        return this._id;
    }

    get Name(): string {
        return this._name;
    }

    get Icon(): string {
        return this._icon;
    }

    get Directory(): DirectoryModel {
        return this._directory;
    }

    set Directory(value: DirectoryModel) {
        this._directory = value;
    }

    Clone(object: any): void {
        this._type = object._type;
        this._id = object._id;
        this._name = object._name;
        this._icon = object._icon;
        this._directory = object._directory;
    }

    static New(type: ProviderType, name: string, icon: string): ProviderModel {
        let provider = new ProviderModel();
        provider._type = type;
        provider._id = Helpers.Guid();
        provider._name = name;
        provider._icon = icon;
        return provider;
    }
}