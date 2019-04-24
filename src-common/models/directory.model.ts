import { BaseModel } from '../base.model';
import { FileModel } from './file.model';

export class DirectoryModel extends BaseModel {
    
    constructor(){
        super();
        this.Directories = [];
        this.Files = [];
    }

    get Name(): string {
        return this.Get('n');
    }

    set Name(value: string) {
        this.Set('n', value);
    }

    get Directories(): DirectoryModel[] {
        return this.Get('d');
    }

    set Directories(value: DirectoryModel[]) {
        this.Set('d', value);
    }

    get Files(): FileModel[] {
        return this.Get('f');
    }

    set Files(value: FileModel[]) {
        this.Set('f', value);
    }
}