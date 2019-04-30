import { serializable, object, list } from 'serializr';
import { FileModel } from './file.model';


export class DirectoryModel extends FileModel {
    private _directories: DirectoryModel[];
    private _files: FileModel[];

    @serializable(list(object(DirectoryModel)))
    get Directories(): DirectoryModel[] {
        return this._directories;
    }

    set Directories(value: DirectoryModel[]) {
        this._directories = value;
    }

    @serializable(list(object(FileModel)))
    get Files(): FileModel[] {
        return this._files;
    }

    set Files(value: FileModel[]) {
        this._files = value;
    }

}