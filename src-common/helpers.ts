import { parse, stringify } from 'flatted';

export class Helpers {

    static Guid(): string {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, (subString) => {
            const random = Math.random() * 16 | 0;
            const value = subString === 'x' ? random : (random & 0x3 | 0x8);
            return value.toString(16);
        });
    }

    static Deserialize<T>(json: string): T {
        return <T>parse(json);
    }

    static Serialize<T>(object: T): string {
        return stringify(object);;
    }
}