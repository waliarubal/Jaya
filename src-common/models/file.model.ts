import { ISerializable } from '../interfaces';

export class FileModel implements ISerializable {
    private _name: string;
    private _size: number;

    get Name(): string {
        return this._name;
    }

    set Name(value: string) {
        this._name = value;
    }

    get Size(): number {
        return this._size;
    }

    set Size(value: number) {
        this._size = value;
    }

    toString(): string {
        return this.Name;
    }

    Serialize(): string {
        return null;
    }

    Deserialize(data: string): FileModel {
        return null;
    }

}