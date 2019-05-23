import { CommandType } from '../interfaces/IMenu';
import { IClonable } from '../interfaces/IClonable';

export class CommandModel implements IClonable {
    private _command: CommandType;
    private _parameter: any;

    constructor(command?: CommandType, parameter?: any) {
        this._command = command;
        this._parameter = parameter;
    }

    get Command(): CommandType {
        return this._command;
    }

    get Parameter(): any {
        return this._parameter;
    }

    Clone(object: any): void {
        this._command = object._command;
        this._parameter = object._parameter;
    }

}