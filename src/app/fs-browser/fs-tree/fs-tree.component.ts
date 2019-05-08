import { Component, EventEmitter, Output } from '@angular/core';
import { BaseComponent } from '@shared/base.component';
import { FileSystemService } from '@services/file-system.service';
import { DirectoryModel, ProviderModel } from '@common/index';
import { TreeNode } from '@shared/controls/tree/tree-node.model';

@Component({
    selector: 'app-fs-tree',
    templateUrl: './fs-tree.component.html',
    providers: [FileSystemService]
})
export class FileSystemTreeComponent extends BaseComponent {
    private readonly _directorySelected: EventEmitter<DirectoryModel>;
    private _nodes: TreeNode[];

    constructor(private _fileSystemService: FileSystemService) {
        super();
        this._directorySelected = new EventEmitter();
        this._nodes = [];
    }

    @Output()
    get OnDirectorySelected(): EventEmitter<DirectoryModel> {
        return this._directorySelected;
    }

    get Nodes(): TreeNode[] {
        return this._nodes;
    }

    protected Initialize(): void {

    }

    protected Destroy(): void {

    }

    async PopulateProviders(): Promise<void> {
        try {
            let fileSystemProvider = await this._fileSystemService.GetProvider();

            let node = new TreeNode();
            node.Label = fileSystemProvider.Name;
            node.Data = fileSystemProvider;
            node.Icon = fileSystemProvider.Icon;

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

    async PopulateNode(node: TreeNode): Promise<void> {
        let children: TreeNode[] = [];

        if (node.Data instanceof ProviderModel) {
            for (let dir of node.Data.Directories) {
                let childNode = new TreeNode();
                childNode.Label = dir.Name;
                childNode.Data = dir;
                childNode.Icon = "fa fa-hdd";
                children.push(childNode);
            }
        }
        else if (node.Data instanceof DirectoryModel) {
            let directory = await this._fileSystemService.GetDirectories(node.Data.Path);
            node.Data = directory;

            for (let dir of directory.Directories) {
                let childNode = new TreeNode();
                childNode.Label = dir.Name;
                childNode.Data = dir;
                children.push(childNode);
            }
        } else
            return;

        node.Children = children;
    }
}