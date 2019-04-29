import { Component, EventEmitter, Output } from '@angular/core';
import { TreeNode } from 'primeng/api';
import { BaseComponent } from '@shared/base.component';
import { FileSystemService } from '@services/file-system.service';
import { DirectoryModel } from '@common/index';

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

    async PopulateNode(node?: TreeNode): Promise<void> {
        let path = node ? node.data.Path : '/';
        let nodes: TreeNode[] = [];

        let directory = await this._fileSystemService.GetDirectories(path);
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

        if (node) {
            node.leaf = true;
            node.children = nodes;
        }
        else
            this._nodes = nodes;
    }
}