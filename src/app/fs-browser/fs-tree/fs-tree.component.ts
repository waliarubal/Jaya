import { Component, EventEmitter, Output } from '@angular/core';
import { TreeNode } from 'primeng/api';
import { BaseComponent } from '@shared/base.component';
import { FileSystemService } from '@services/file-system.service';
import { DirectoryModel, ProviderModel } from '@common/index';

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
    }

    get Nodes(): TreeNode[] {
        return this._nodes;
    }

    @Output()
    get DirectorySelected(): EventEmitter<DirectoryModel> {
        return this._directorySelected;
    }

    protected Initialize(): void {

    }

    protected Destroy(): void {

    }

    async PopulateProviders(): Promise<void> {
        let nodes: TreeNode[] = [];

        try {
            let fileSystemProvider = await this._fileSystemService.GetProvider();
            let node = <TreeNode>{
                label: fileSystemProvider.Name,
                data: fileSystemProvider,
                icon: fileSystemProvider.Icon,
                leaf: false
            };
            nodes.push(node);
        } catch (ex) {
            console.log(ex);
        }

        this._nodes = nodes;
    }

    OnNodeSelected(event: any): void {
        let node = event.node;
        if (node && node.data instanceof DirectoryModel)
            this.DirectorySelected.emit(node.data);
    }

    async PopulateNode(node?: TreeNode): Promise<void> {
        if (!node)
            return;

        let nodes: TreeNode[] = [];

        if (node.data instanceof ProviderModel) {
            node.data.Directories.forEach(directoryModel => {
                let node = <TreeNode>{
                    label: directoryModel.Name,
                    data: directoryModel,
                    expandedIcon: "fa fa-folder-open",
                    collapsedIcon: "fa fa-folder",
                    leaf: false
                };
                nodes.push(node);
            });
        }
        else if (node.data instanceof DirectoryModel) {
            let directory = await this._fileSystemService.GetDirectories(node.data.Path);
            
            for (let directoryModel of directory.Directories) {
                let node = <TreeNode>{
                    label: directoryModel.Name,
                    data: directoryModel,
                    expandedIcon: "fa fa-folder-open",
                    collapsedIcon: "fa fa-folder",
                    leaf: false
                };
                nodes.push(node);
            }
        }

        if (node) {
            node.leaf = true;
            node.children = nodes;
        }
    }
}