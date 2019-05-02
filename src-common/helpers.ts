import { deserialize, serialize, ClazzOrModelSchema } from 'serializr';

export class Helpers {

    static Guid(): string {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, (subString) => {
            const random = Math.random() * 16 | 0;
            const value = subString === 'x' ? random : (random & 0x3 | 0x8);
            return value.toString(16);
        });
    }

    static async Deserialize<T>(modelSchema: ClazzOrModelSchema<T>, json: any): Promise<T> {
        return new Promise<T>((resolve, reject) => {
            deserialize<T>(modelSchema, json, (error: any, result: T) => {
                if (error)
                    reject(error);
                else
                    resolve(result);
            });
        });
    }

    static Serialize<T>(modelSchema: ClazzOrModelSchema<T>, object: T): string {
        return serialize<T>(modelSchema, object);
    }
}