export class FileModel {
    private _name: string;
    private _size: number;
    private _path: string;

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

    toString(): string {
        return this.Name;
    }

}