import { Helpers } from '../helpers';
import { ISerializable } from '../interfaces';

export enum MessageType {
    Unknown = 'u',
    Handshake = 'h',
    Directoties = 'd',
    Files = 'f'
}

export class MessageModel {
    private _id: string;
    private readonly _type: MessageType;
    private readonly _data: ISerializable;

    private constructor(type: MessageType, data?: ISerializable) {
        this._type = type;
        this._data = data;
    }

    get Id(): string {
        return this._id;
    }

    set Id(value: string) {
        this._id = value;
    }

    get Type(): MessageType {
        return this._type;
    }

    get Data(): ISerializable {
        return this._data;
    }

    toString(): string {
        return `Message Type: ${this.Type}\r\nMessage Data: ${this.Data}`;
    }

    static Empty(): MessageModel {
        let message = new MessageModel(MessageType.Unknown);
        return message;
    }

    static New(type: MessageType, data?: ISerializable): MessageModel {
        let message = new MessageModel(type, data);
        message.Id = Helpers.Guid();
        return message;
    }

}