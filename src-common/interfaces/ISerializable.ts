export interface ISerializable {

    Serialize(): string;

    Deserialize(data: string): any;

}