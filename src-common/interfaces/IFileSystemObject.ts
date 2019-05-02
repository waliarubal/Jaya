export enum FileSystemObjectType {
    File,
    Directory,
    Drive
}

export interface IFileSystemObject {
    Id: string;
    Name: string;
    Path: string;
    Size: number;
    Type: FileSystemObjectType
}