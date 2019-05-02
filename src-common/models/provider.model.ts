import { list, object, primitive, identifier } from 'serializr';
import { Helpers } from '../helpers';
import { DirectoryModel, DirectoryModelSchema } from './directory.model';

export const ProviderModelSchema = {
    factory: contect => new ProviderModel(),
    props: {
        _id: identifier(),
        _name: primitive(),
        _icon: primitive(),
        Directories: list(object(DirectoryModelSchema))
    }
};

export class ProviderModel {
    private _id: string;
    private _name: string;
    private _icon: string;
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