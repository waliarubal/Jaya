import { serializable } from 'serializr';

export class FileModel {
    private _name: string;
    private _size: number;

    @serializable
    get Name(): string {
        return this._name;
    }

    
    set Name(value: string) {
        this._name = value;
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