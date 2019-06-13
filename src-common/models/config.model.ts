import { IClonable } from '../interfaces/IClonable';

export class ConfigModel implements IClonable {
    private _key: number;
    private _value: any;

    constructor(key?: number, value?: any){
        this._key = key;
        this._value = value;
    }

    get Key(): number {
        return this._key;
    }

    get Value(): any {
        return this._value;
    }

    set Value(value: any) {
        this._value = value;
    }

    Clone(object: any): void {
        this._key = object._key;
        this._value = object._value;
    }

}