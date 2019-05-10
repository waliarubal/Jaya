import { Component, EventEmitter, Output, Input } from '@angular/core';
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
    private _providers: ProviderModel[];

    constructor(private _fileSystemService: FileSystemService) {
        super();
        this._directorySelected = new EventEmitter();
        this._nodes = [];
    }

    @Input()
    get Providers(): ProviderModel[] {
        return this._providers;
    }

    set Providers(value: ProviderModel[]) {
        this._providers = value;
        if (value) {
            this.Nodes.length = 0;
            for (let provider of value) {
                let node = <ITreeNode>{
                    text: provider.Name,
                    iconCls: provider.Icon,
                    state: TreeNodeState.Close,
                    attributes: provider
                };
                this.Nodes.push(node);
            }
        }
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

    OnNodeSelected(node: ITreeNode): void {
        if (node && node.attributes instanceof DirectoryModel)
            this.OnDirectorySelected.emit(node.attributes);
    }

    async PopulateNode(node: ITreeNode): Promise<void> {
        if (node.state !== TreeNodeState.Close && node.children)
            return;

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
                    iconCls: "fa fa-folder",
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