import { list, primitive } from 'serializr';
import { FileModel, FileModelSchema } from './file.model';

export const DirectoryModelSchema = {
    factory: context => new DirectoryModel(),
    extends: FileModelSchema,
    props: {
        Directories: list(primitive()),
        Files: list(primitive())
    }
};

export class DirectoryModel extends FileModel {
    private _directories: string[];
    private _files: string[];

    get Directories(): string[] {
        return this._directories;
    }

    set Directories(value: string[]) {
        this._directories = value;
    }

    get Files(): string[] {
        return this._files;
    }

    set Files(value: string[]) {
        this._files = value;
    }

}