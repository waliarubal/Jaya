import { serializable, list, object } from 'serializr';
import { Helpers, DirectoryModel } from '../index';

export class ProviderModel {
    @serializable private _id: string;
    @serializable private _name: string;
    @serializable private _icon: string;
    private _directories: DirectoryModel[];

    get Id(): string {
        return this._id;
    }


    get Name(): string {
        return this._name;
    }

    get Icon(): string {
        return this._icon;
    }

    @serializable(list(object(DirectoryModel)))
    get Directories(): DirectoryModel[] {
        return this._directories;
    }

    set Directories(value: DirectoryModel[]) {
        this._directories = value;
    }

    static New(name: string, icon: string): ProviderModel {
        let provider = new ProviderModel();
        provider._id = Helpers.Guid();
        provider._name = name;
        provider._icon = icon;
        return provider;
    }
}