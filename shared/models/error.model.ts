import { BaseModel } from '../base.model';

export class ErrorModel extends BaseModel {

    constructor(message: string = null) {
        super();
        this.Message = message;
    }

    get Message(): string {
        return this.Get('msg');
    }

    set Message(value: string) {
        this.Set('msg', value);
    }

    toString(): string {
        return this.Message;
    }

}