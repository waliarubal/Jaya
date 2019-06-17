import { IClonable } from '../interfaces/IClonable';
import { ProviderType } from './provider.model'

export class AccountModel implements IClonable {
    private _name: string;
    private _token: string;
    private _type: ProviderType;

    constructor(name?: string, type?: ProviderType, token?: string) {
        this._name = name;
        this._type = type;
        this._token = token;
    }

    get Name(): string {
        return this._name;
    }

    get Type(): ProviderType {
        return this._type;
    }

    get Token(): string {
        return this._token;
    }

    Clone(object: any): void {
        if (!object)
            return;
            
        this._name = object._name;
        this._type = object._type;
        this._token = object._token;
    }

}