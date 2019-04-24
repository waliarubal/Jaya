import { BaseModel } from '../base.model';

export class FileModel extends BaseModel {
    
    get Name(): string {
        return this.Get('n');
    }

    set Name(value: string) {
        this.Set('n', value);
    }

    get Size(): number {
        return this.Get('s');
    }

    set Size(value: number) {
        this.Set('s', value);
    }
    
}