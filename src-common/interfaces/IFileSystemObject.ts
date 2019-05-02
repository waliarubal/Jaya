export enum FileSystemObjectType {
    File = 'File',
    Directory = 'Directory',
    Drive = 'Drive'
}

export interface IFileSystemObject {
    Id: string;
    Name: string;
    Path: string;
    Size: number;
    Type: FileSystemObjectType
}