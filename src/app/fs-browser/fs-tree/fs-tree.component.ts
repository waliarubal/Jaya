import { Component, EventEmitter, Output } from '@angular/core';
import { of, Observable } from 'rxjs';
import { BaseComponent } from '@shared/base.component';
import { FileSystemService } from '@services/file-system.service';
import { DirectoryModel, ProviderModel } from '@common/index';
import { FileSystemTreeNode } from './fs-tree-node.model';

@Component({
    selector: 'app-fs-tree',
    templateUrl: './fs-tree.component.html',
    providers: [FileSystemService]
})
export class FileSystemTreeComponent extends BaseComponent {
    private readonly _directorySelected: EventEmitter<DirectoryModel>;
    private _nodes: FileSystemTreeNode[];

    constructor(private _fileSystemService: FileSystemService) {
        super();
        this._directorySelected = new EventEmitter();
    }

    @Output()
    get OnDirectorySelected(): EventEmitter<DirectoryModel> {
        return this._directorySelected;
    }

    get Nodes(): FileSystemTreeNode[] {
        return this._nodes;
    }

    protected Initialize(): void {

    }

    protected Destroy(): void {

    }

    async PopulateProviders(): Promise<void> {
        try {
            let fileSystemProvider = await this._fileSystemService.GetProvider();
            let node = <FileSystemTreeNode>{
                Label: fileSystemProvider.Name,
                Data: fileSystemProvider,
                Icon: fileSystemProvider.Icon,
                Children: []
            };
            this._nodes = [node];
        } catch (ex) {
            console.log(ex);
        }
    }

    OnNodeSelected(event: any): void {
        let node = event.node;
        if (node && node.data instanceof DirectoryModel)
            this.OnDirectorySelected.emit(node.data);
    }

    IsHavingChildNodes(node: FileSystemTreeNode): boolean {
        console.log(node);
        
        if (node.Data instanceof ProviderModel)
            return true;

        return node.Children && node.Children.length > 0;
    }

    async PopulateNode(node: FileSystemTreeNode): Promise<Observable<FileSystemTreeNode[]>> {
        console.log(node);
        if (node.Data instanceof ProviderModel) {
            node.Data.Directories.forEach(fileName => {
                let node = <FileSystemTreeNode>{
                    Label: fileName.Name,
                    Data: fileName,
                    Icon: "fa fa-hdd",
                    Children: []
                };
                node.Children.push(node);
            });
        }
        else if (node.Data instanceof DirectoryModel) {
            let directory = await this._fileSystemService.GetDirectories(node.Data.Path);
            node.Data = directory;

            for (let fileName of directory.Directories) {
                let node = <FileSystemTreeNode>{
                    Label: fileName.Name,
                    Data: fileName,
                    // expandedIcon: "fa fa-folder-open",
                    // collapsedIcon: "fa fa-folder",
                    Children: []
                };
                node.Children.push(node);
            }
        }

        return of(node.Children);
    }
}