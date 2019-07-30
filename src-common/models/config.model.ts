import { IClonable } from '../interfaces/IClonable';

export class ConfigModel implements IClonable {
    private _key: number;
    private _value: string;

    constructor(key?: number, value?: any){
        this._key = key;
        this._value = value;
    }

    get Key(): number {
        return this._key;
    }

    get Value(): string {
        return this._value;
    }

    set Value(value: string) {
        this._value = value;
    }

    Clone(object: any): void {
        this._key = object._key;
        this._value = object._value;
    }

}