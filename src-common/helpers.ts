import { parse, stringify } from 'flatted';
import { ISerializable } from './interfaces/ISerializable';

export class Helpers {

    static Guid(): string {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, (subString) => {
            const random = Math.random() * 16 | 0;
            const value = subString === 'x' ? random : (random & 0x3 | 0x8);
            return value.toString(16);
        });
    }

    static Deserialize<T extends ISerializable>(json: string, type: new () => T): T {
        let source = parse(json);
        
        let destination = new type();
        destination.Deserialize(source);
        return destination;
    }

    static Serialize<T extends ISerializable>(object: T): string {
        return stringify(object);;
    }
}