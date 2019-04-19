import { BaseModel } from '@shared/models/base.model';

export class FileModel extends BaseModel {
    
    get Name(): string {
        return this.Get('name');
    }

    set Name(value: string) {
        this.Set('name', value);
    }
    
}