import { primitive, identifier, list } from 'serializr';
import { Helpers } from '../helpers';

export const ProviderModelSchema = {
    factory: contect => new ProviderModel(),
    props: {
        _id: identifier(),
        _name: primitive(),
        _icon: primitive(),
        Directories: list(primitive())
    }
};

export class ProviderModel {
    private _id: string;
    private _name: string;
    private _icon: string;
    private _directories: string[];

    get Id(): string {
        return this._id;
    }

    get Name(): string {
        return this._name;
    }

    get Icon(): string {
        return this._icon;
    }

    get Directories(): string[] {
        return this._directories;
    }

    set Directories(value: string[]) {
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