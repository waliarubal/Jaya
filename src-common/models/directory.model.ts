import { serializable, object, list } from 'serializr';
import { FileModel } from './file.model';

export class DirectoryModel extends FileModel {
    @serializable(list(object(DirectoryModel))) private _directories: DirectoryModel[];
    @serializable(list(object(FileModel))) private _files: FileModel[];
    
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

}