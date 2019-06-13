export class Pair<F, S> {
    private _first: F;
    private _second: S;

    constructor(first?: F, second?: S) {
        if (first)
            this.First = first;
        if (second)
            this._second = second;
    }

    get First(): F {
        return this._first;
    }

    set First(value: F) {
        this._first = value;
    }

    get Second(): S {
        return this._second;
    }

    set Second(value: S) {
        this._second = value;
    }
}