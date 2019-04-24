import { FileModel } from './file.model';
import { ISerializable } from '../interfaces';

export class DirectoryModel extends FileModel implements ISerializable {
    private _directories: DirectoryModel[];
    private _files: FileModel[];

    get Directories(): DirectoryModel[] {
        return this._directories;
    }

    set Directories(value: DirectoryModel[]) {
        this._directories = value;
    }

    get Files(): FileModel[] {
        return this._files;
    }

    set Files(value: FileModel[]) {
        this._files = value;
    }

    Serialize(): string {
        return null;
    }

    Deserialize(data: string): DirectoryModel {
        return null;
    }
}