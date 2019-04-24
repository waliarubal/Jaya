import { FileModel } from './file.model';
import { ISerializable } from '../interfaces/ISerializable';

export class DirectoryModel extends FileModel implements ISerializable<DirectoryModel> {
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
        return `{
            "name": "${this.Name}",
            "size": ${this.Size}
        }`;
    }

    Deserialize(data: string): DirectoryModel {
        let obj = JSON.parse(data);
        
        let dir = new DirectoryModel();
        dir.Name = obj.name;
        dir.Size = obj.size;
        return dir;
    }

}