import { FileModel } from './file.model';
import { ISerializable } from '../interfaces/ISerializable';

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

    Deserialize(object: any){
        super.Deserialize(object);
        
        this.Directories = [];
        if (object._directories) {
            for (let dirObj of object._directories) {
                let directory = new DirectoryModel();
                directory.Deserialize(dirObj);
                this.Directories.push(directory);
            }
        }

        this.Files = [];
        if (object._files) {
            for (let fileObj of object._files) {
                let file = new FileModel();
                file.Deserialize(fileObj);
                this.Files.push(file);
            }
        }
    }

}