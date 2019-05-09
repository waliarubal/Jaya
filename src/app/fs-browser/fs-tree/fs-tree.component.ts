import { Component, EventEmitter, Output } from '@angular/core';
import { BaseComponent } from '@shared/base.component';
import { FileSystemService } from '@services/file-system.service';
import { DirectoryModel, ProviderModel } from '@common/index';

enum TreeNodeState {
    Open = 'open',
    Close = 'closed'
}

interface ITreeNode {
    readonly id: number;
    readonly text: string;
    readonly iconCls: string;
    checked: boolean;
    state: TreeNodeState;
    attributes: any;
    children: ITreeNode[];
}

@Component({
    selector: 'app-fs-tree',
    templateUrl: './fs-tree.component.html',
    providers: [FileSystemService]
})
export class FileSystemTreeComponent extends BaseComponent {
    private readonly _directorySelected: EventEmitter<DirectoryModel>;
    private _nodes: ITreeNode[];

    constructor(private _fileSystemService: FileSystemService) {
        super();
        this._directorySelected = new EventEmitter();
        this._nodes = [];
    }

    @Output()
    get OnDirectorySelected(): EventEmitter<DirectoryModel> {
        return this._directorySelected;
    }

    get Nodes(): ITreeNode[] {
        return this._nodes;
    }

    protected Initialize(): void {

    }

    protected Destroy(): void {

    }

    async PopulateProviders(): Promise<void> {
        try {
            let fileSystemProvider = await this._fileSystemService.GetProvider();

            let node = <ITreeNode>{
                text: fileSystemProvider.Name,
                iconCls: fileSystemProvider.Icon,
                attributes: fileSystemProvider,
                state: TreeNodeState.Close
            };

            this._nodes = [node];
        } catch (ex) {
            console.log(ex);
        }
    }

    OnNodeSelected(node: ITreeNode): void {
        if (node && node.attributes instanceof DirectoryModel)
            this.OnDirectorySelected.emit(node.attributes);
    }

    async PopulateNode(node: ITreeNode): Promise<void> {
        let children: ITreeNode[] = [];

        if (node.attributes instanceof ProviderModel) {
            for (let dir of node.attributes.Directories) {
                let childNode = <ITreeNode>{
                    text: dir.Name,
                    iconCls: "fa fa-hdd",
                    attributes: dir,
                    state: TreeNodeState.Close
                };
                children.push(childNode);
            }
        }
        else if (node.attributes instanceof DirectoryModel) {
            let directory = await this._fileSystemService.GetDirectories(node.attributes.Path);
            node.attributes = directory;

            for (let dir of directory.Directories) {
                let childNode = <ITreeNode>{
                    text: dir.Name,
                    attributes: dir,
                    state: TreeNodeState.Close
                };
                children.push(childNode);
            }
        } else
            return;

        node.state = TreeNodeState.Open;
        node.children = children;
    }
}