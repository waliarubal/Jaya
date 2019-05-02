import { parse, stringify } from 'flatted';
import { IClonable } from './interfaces/IClonable';

export class Helpers {

    static Guid(): string {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, (subString) => {
            const random = Math.random() * 16 | 0;
            const value = subString === 'x' ? random : (random & 0x3 | 0x8);
            return value.toString(16);
        });
    }

    static Deserialize<T extends IClonable>(json: string, type: new () => T): T {
        let source = parse(json);
        
        let destination = new type();
        destination.Clone(source);
        return destination;
    }

    static Serialize<T extends IClonable>(object: T): string {
        return stringify(object);;
    }
}