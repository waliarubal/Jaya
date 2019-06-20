import * as Datastore from 'nedb';
import { isNullOrUndefined } from 'util';

export class DataBase<T> {
    private _fileName: string;
    private _db: Datastore;

    get IsOpen(): boolean {
        return !isNullOrUndefined(this._db);
    }

    get FileName(): string {
        return this._fileName;
    }

    Open(fileName: string): void {
        this._fileName = fileName;
        this._db = new Datastore({
            autoload: true,
            inMemoryOnly: false,
            timestampData: false,
            filename: fileName
        });
    }

    Close(): void {
        if (this._db)
            this._db.persistence.compactDatafile();
    }

    Find(query: any): Promise<T[]> {
        return new Promise<T[]>((resolve, reject) => {
            if (!this.IsOpen)
                reject(new Error('Database connection not open.'));

            this._db.find(query, (err: Error, documents: T[]) => {
                if (err)
                    reject(err);
                else
                    resolve(documents);
            });
        });
    }

    Insert(record: T): Promise<T> {
        return new Promise((resolve, reject) => {
            if (!this.IsOpen)
                reject(new Error('Database connection not open.'));

            this._db.insert<T>(record, (err: Error, document: T) => {
                if (err)
                    reject(err);
                else
                    resolve(document);
            });
        });
    }

    Update(query: any, record: T): Promise<T> {
        return new Promise((resolve, reject) => {
            if (!this.IsOpen)
                reject(new Error('Database connection not open.'));

            this._db.update<T>(query, record, { multi: false, upsert: true, returnUpdatedDocs: true }, (err: Error, count: number, document: any, upsert: boolean) => {
                if (err)
                    reject(err);
                else
                    resolve(document);
            });
        });
    }

    Delete(query: any): Promise<number> {
        return new Promise((resolve, reject) => {
            if (!this.IsOpen)
                reject(new Error('Database connection not open.'));

            this._db.remove(query, { multi: false }, (err: Error, count: number) => {
                if (err)
                    reject(err);
                else
                    resolve(count);
            });
        });
    }

}