import { BaseModel } from '@shared/base.model';

export enum MessageType {
    Handshake
}

export class MessageModel extends BaseModel {

    constructor(type: MessageType, data?: any) {
        super();
        this.Set('t', type);
        this.Set('d', data);
    }

    get Type(): MessageType {
        return this.Get('t');
    }

    get Data(): any {
        return this.Get('d');
    }

    GetData<T>(): T {
        return <T>this.Data;
    }

}