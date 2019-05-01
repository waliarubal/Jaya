import { serializable } from 'serializr';

export class ErrorModel {
    @serializable private _message: string;

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

}