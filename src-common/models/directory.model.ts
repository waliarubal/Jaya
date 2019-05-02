import { FileModel } from './file.model';
import { IClonable } from '../interfaces/IClonable';
import { IFileSystemObject, FileSystemObjectType } from '../interfaces/IFileSystemObject';

export class DirectoryModel extends FileModel implements IClonable {
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

    Clone(object: any){
        super.Clone(object);
        
        this.Directories = [];
        if (object._directories) {
            for (let dirObj of object._directories) {
                let directory = new DirectoryModel();
                directory.Clone(dirObj);
                this.Directories.push(directory);
            }
        }

        this.Files = [];
        if (object._files) {
            for (let fileObj of object._files) {
                let file = new FileModel();
                file.Clone(fileObj);
                this.Files.push(file);
            }
        }
    }

    GetObjects(): IFileSystemObject[] {
        let objects: IFileSystemObject[] = [];
        
        if (this.Directories) {
            for(let dir of this.Directories) {
                let object = <IFileSystemObject>{
                    Name: dir.Name,
                    Path: dir.Path,
                    Type: FileSystemObjectType.Directory
                }
                objects.push(object);
            }
        }
        
        if (this.Files) {
            for(let file of this.Files) {
                let object = <IFileSystemObject>{
                    Name: file.Name,
                    Path: file.Path,
                    Type: FileSystemObjectType.File,
                    Size: file.Size
                }
                objects.push(object);
            }
        }
            
        return objects;
    }

}