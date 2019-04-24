export interface ISerializable<T> {
    Serialize(): string;
    Deserialize(data: string): T;
}