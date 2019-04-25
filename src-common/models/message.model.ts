import { Helpers } from '../helpers';

export enum MessageType {
    Unknown = 'u',
    Handshake = 'h',
    Directoties = 'd',
    Files = 'f'
}

export class MessageModel {
    private readonly _type: MessageType;
    private _id: string;
    private _dataJson: string;

    private constructor(type: MessageType, dataJson: string = '') {
        this._type = type;
        this._dataJson = dataJson;
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

    get DataJson(): string {
        return this._dataJson;
    }

    set DataJson(value: string) {
        this._dataJson = value;
    }

    toString(): string {
        return `Message Type: ${this.Type}\r\nMessage Data JSON: ${this.DataJson}`;
    }

    static Empty(): MessageModel {
        let message = new MessageModel(MessageType.Unknown);
        return message;
    }

    static New(type: MessageType, data: string = ''): MessageModel {
        let message = new MessageModel(type, data);
        message.Id = Helpers.Guid();
        return message;
    }

}