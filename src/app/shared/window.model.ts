import { BaseModel } from '@common/index';
import { Type } from '@angular/core';

export class WindowModel extends BaseModel {

    get Title(): string {
        return this.Get('t');
    }

    set Title(value: string) {
        this.Set('t', value);
    }

    get IsModal(): boolean {
        return this.Get('is_m');
    }

    set IsModal(value: boolean) {
        this.Set('is_m', value);
    }

    get Component(): Type<any> {
        return this.Get('c');
    }

    set Component(value: Type<any>) {
        this.Set('c', value);
    }

    static Empty(): WindowModel {
        let window = new WindowModel();
        window.Title = '';
        window.IsModal = false;
        window.Component = undefined;
        return window;
    }
}