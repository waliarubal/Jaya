import { IClonable } from '../interfaces/IClonable';

export class FileModel implements IClonable {
    private _name: string;
    private _size: number;
    private _path: string;
    private _accessed: Date;
    private _modified: Date;
    private _created: Date;

    get Name(): string {
        return this._name;
    }

    set Name(value: string) {
        this._name = value;
    }

    get Path(): string {
        return this._path;
    }

    set Path(value: string) {
        this._path = value;
    }

    get Size(): number {
        return this._size;
    }

    set Size(value: number) {
        this._size = value;
    }

    get Accessed(): Date {
        return this._accessed;
    }

    set Accessed(value: Date) {
        this._accessed = value;
    }

    get Modified(): Date {
        return this._modified;
    }

    set Modified(value: Date) {
        this._modified = value;
    }

    get Created(): Date {
        return this._created;
    }

    set Created(value: Date) {
        this._created = value;
    }

    toString(): string {
        return this.Name;
    }

    Clone(object: any): void {
        this.Name = object._name;
        this.Size = object._size;
        this.Path = object._path;
        this.Accessed = object._accessed;
        this.Modified = object._modified;
        this.Created = object._created;
    }

}