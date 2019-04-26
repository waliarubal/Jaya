import { serializable } from 'serializr';

export class FileModel {
    private _name: string;
    private _size: number;
    private _path: string;

    @serializable
    get Name(): string {
        return this._name;
    }

    
    set Name(value: string) {
        this._name = value;
    }

    @serializable
    get Path(): string {
        return this._path;
    }

    set Path(value: string) {
        this._path = value;
    }

    @serializable
    get Size(): number {
        return this._size;
    }

    set Size(value: number) {
        this._size = value;
    }

    toString(): string {
        return this.Name;
    }

}