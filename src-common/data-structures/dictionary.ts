export class Dictionary<K, V> {
    private readonly _map: Map<K, V>;

    constructor() {
        this._map = new Map<K, V>();
    }

    get Keys(): K[] {
        return Array.from(this._map.keys());
    }

    get Values(): V[] {
        return Array.from(this._map.values());
    }

    get Length(): number {
        return this.Keys.length;
    }

    IsHaving(key: K): boolean {
        return this._map.has(key);
    }

    Set(key: K, value: V): void {
        this._map.set(key, value);
    }

    Get(key: K, deleteItem: boolean = false): V {
        if (!this.IsHaving(key))
            return null;

        let value = this._map.get(key);
        if (deleteItem)
            this.Delete(key);

        return value;
    }

    Delete(key: K): boolean {
        if (this.IsHaving(key))
            return this._map.delete(key);

        return false;
    }

    Clear(): void {
        this._map.clear();
    }
}