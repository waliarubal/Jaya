import { ISerializable } from './interfaces/ISerializable'
import { Constants } from './constants';

export class Helpers {

    static Guid(): string {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, (subString) => {
            const random = Math.random() * 16 | 0;
            const value = subString === 'x' ? random : (random & 0x3 | 0x8);
            return value.toString(16);
        });
    }

    static Serialize<T>(data: ISerializable<T>[]): string {
        if (!data || data.length === 0)
            return '';

        let string = '';
        data.forEach(item => {
            string = `${string}${Constants.SEPARATOR}${item.Serialize()}`;
        });
        return string;
    }

    static Deserialize<T>(data: string): T[] {
        if (!data)
            return [];

        const prototype = new Object() as ISerializable<T>;

        let items: T[] = [];
        data.split(Constants.SEPARATOR).forEach(string => items.push(prototype.Deserialize(string)));
        return items;
    }

}