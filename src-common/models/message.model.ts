export enum MessageType {
    Unknown = 'u',
    Handshake = 'h'
}

export class MessageModel {
    private readonly _type: MessageType;
    private readonly _data: any;

    constructor(type: MessageType, data?: any) {
        this._type = type;
        this._data = data;
    }

    get Type(): MessageType {
        return this._type;
    }

    get Data(): any {
        return this._data;
    }

    GetData<T>(): T {
        return <T>this.Data;
    }

    toString(): string {
        return `Message Type: ${this.Type}\r\nMessage Data: ${this.Data}`;
    }

    static Empty(): MessageModel {
        return new MessageModel(MessageType.Unknown);
    }

}