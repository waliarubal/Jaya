import { IClonable } from '../interfaces/IClonable';

export class ErrorModel implements IClonable {
    private _message: string;

    constructor(message: string = null) {
        this.Message = message;
    }

    get Message(): string {
        return this._message;
    }

    set Message(value: string) {
        this._message = value;
    }

    toString(): string {
        return this.Message;
    }

    Clone(object: any): void {
        this.Message = object._message;
    }

}