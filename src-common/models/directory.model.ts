import { object, list } from 'serializr';
import { FileModel, FileModelSchema } from './file.model';

export const DirectoryModelSchema = {
    factory: context => new DirectoryModel(),
    extends: FileModelSchema,
    props: {
        // Directories: list(object(DirectoryModelSchema)),
        Files: list(object(FileModelSchema))
    }
};

export class DirectoryModel extends FileModel {
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

}