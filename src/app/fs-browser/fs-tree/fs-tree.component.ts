import { Component, EventEmitter, Output, ViewChild } from '@angular/core';
import { jqxTreeComponent } from 'jqwidgets-ng/jqxtree/angular_jqxtree';
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
    @ViewChild('dirTree') private _dirTree: jqxTreeComponent;

    constructor(private _fileSystemService: FileSystemService) {
        super();
        this._directorySelected = new EventEmitter();
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

        try {
            let fileSystemProvider = await this._fileSystemService.GetProvider();
            let node = {
                label: fileSystemProvider.Name,
                data: fileSystemProvider,
                icon: fileSystemProvider.Icon,
                expanded: false
            };
            this._dirTree.addTo(node, null);
            // nodes.push(node);

            
        } catch (ex) {
            console.log(ex);
        }
    }

    OnNodeSelected(event: any): void {
        let node = event.node;
        if (node && node.data instanceof DirectoryModel)
            this.DirectorySelected.emit(node.data);
    }

    async PopulateNode(node?: any): Promise<void> {
        if (!node)
            return;

        if (node.data instanceof ProviderModel) {
            node.data.Directories.forEach(fileName => {
                let node = {
                    label: fileName.Name,
                    data: fileName,
                    icon: "fa fa-hdd",
                    expanded: false
                };
                //nodes.push(node);
            });
        }
        else if (node.data instanceof DirectoryModel) {
            let directory = await this._fileSystemService.GetDirectories(node.data.Path);
            node.data = directory;

            for (let fileName of directory.Directories) {
                let node = {
                    label: fileName.Name,
                    data: fileName,
                    expandedIcon: "fa fa-folder-open",
                    collapsedIcon: "fa fa-folder",
                    expanded: false
                };
                //nodes.push(node);
            }
        }

        if (node) {
            node.expanded = true;
            //node.children = nodes;
        }
    }
}