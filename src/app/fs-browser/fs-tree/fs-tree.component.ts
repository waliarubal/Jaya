import { Component, EventEmitter, Output, Input } from '@angular/core';
import { BaseComponent } from '@shared/base.component';
import { DirectoryModel, ProviderModel, ConfigModel, IProviderService } from '@common/index';
import { ConfigService } from '@services/config.service';
import { ProviderService } from '@services/provider.service';

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

interface ITreeNodeData {
    readonly ProviderService: IProviderService;
    Object: any;
    IsPopulated: boolean;
}

@Component({
    selector: 'app-fs-tree',
    templateUrl: './fs-tree.component.html',
    providers: [ProviderService]
})
export class FileSystemTreeComponent extends BaseComponent {
    private readonly _directorySelected: EventEmitter<DirectoryModel>;
    private _nodes: ITreeNode[];
    private _providers: ProviderModel[];

    constructor(config: ConfigService, private readonly _providerService: ProviderService) {
        super(config);
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
                let nodeData = <ITreeNodeData>{
                    ProviderService: this._providerService.GetService(provider.Type),
                    Object: provider,
                    IsPopulated: false
                };
                let node = <ITreeNode>{
                    text: provider.Name,
                    iconCls: provider.Icon,
                    state: TreeNodeState.Close,
                    attributes: nodeData
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

    protected async Initialize(): Promise<void> {

    }

    protected async Destroy(): Promise<void> {

    }

    protected OnConfigChanged(config: ConfigModel): void {

    }

    private RaiseDirectorySelected(nodeData: ITreeNodeData): void {
        let directory: DirectoryModel;
        if (nodeData.Object instanceof ProviderModel)
            directory = nodeData.Object.Directory;
        else if (nodeData.Object instanceof DirectoryModel)
            directory = nodeData.Object;
        else
            return;

        this.OnDirectorySelected.emit(directory);
    }

    OnNodeSelected(node: ITreeNode): void {
        if (node && node.attributes) {
            let nodeData = <ITreeNodeData>node.attributes;
            if (nodeData.IsPopulated)
                this.RaiseDirectorySelected(nodeData);
        }
    }

    async PopulateNode(node: ITreeNode): Promise<void> {
        if (node.state !== TreeNodeState.Close && node.children)
            return;

        let children: ITreeNode[] = [];
        let nodeData = <ITreeNodeData>node.attributes;

        if (nodeData.IsPopulated)
            return;

        if (nodeData.Object instanceof ProviderModel) {
            let directories: DirectoryModel[];
            if (nodeData.Object.Directory)
                directories = nodeData.Object.Directory.Directories;
            else {
                const dirs = await nodeData.ProviderService.GetDirectory(undefined);
                directories = dirs.Directories;
            }

            nodeData.IsPopulated = true;

            for (let dir of directories) {
                let childNodeData = <ITreeNodeData>{
                    ProviderService: nodeData.ProviderService,
                    Object: dir
                };
                let childNode = <ITreeNode>{
                    text: dir.Name,
                    iconCls: "fa fa-hdd",
                    attributes: childNodeData,
                    state: TreeNodeState.Close
                };
                children.push(childNode);
            }
        }
        else if (nodeData.Object instanceof DirectoryModel) {
            let directory = await nodeData.ProviderService.GetDirectory(nodeData.Object.Path);
            nodeData.Object = directory;

            nodeData.IsPopulated = true;

            for (let dir of directory.Directories) {
                let childNodeData = <ITreeNodeData>{
                    ProviderService: nodeData.ProviderService,
                    Object: dir
                };
                let childNode = <ITreeNode>{
                    text: dir.Name,
                    attributes: childNodeData,
                    iconCls: "fa fa-folder",
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