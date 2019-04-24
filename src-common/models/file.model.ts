import { ISerializable } from '../interfaces/ISerializable';

export class FileModel implements ISerializable<FileModel> {
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
        return `{
            "name": "${this.Name}",
            "size": ${this.Size}
        }`;
    }

    Deserialize(data: string): FileModel {
        let obj = JSON.parse(data);
        
        let file = new FileModel();
        file.Name = obj.name;
        file.Size = obj.size;
        return file;
    }

}