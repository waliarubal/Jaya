import { Dictionary } from './data-structures';

export abstract class BaseModel {
    private readonly _variables: Dictionary<string, any>;

    constructor() {
        this._variables = new Dictionary();
    }

    protected Get<T>(name: string, deleteVariable: boolean = false): T {
        return <T>this._variables.Get(name, deleteVariable);
    }

    protected Set<T>(name: string, value: T): void {
        this._variables.Set(name, value);
    }

    protected Delete(name: string): boolean {
        return this._variables.Delete(name);
    }

    protected Clear(): void {
        this._variables.Clear();
    }
}