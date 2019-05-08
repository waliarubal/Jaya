import { Component, Input, EventEmitter, Output, ViewChild } from '@angular/core';

import { TreeNode } from './tree-node.model';

@Component({
    selector: 'ctrl-tree',
    templateUrl: './tree.component.html',
    styleUrls: ['./tree.component.css']
})
export class TreeComponent {
    private readonly _nodeClicked: EventEmitter<TreeNode>;
    private readonly _nodeExpanding: EventEmitter<TreeNode>;
    private _nodes: TreeNode[];

    constructor() {
        this._nodeClicked = new EventEmitter();
        this._nodeExpanding = new EventEmitter();
    }

    @Input()
    get Nodes(): TreeNode[] {
        return this._nodes;
    }

    set Nodes(value: TreeNode[]) {
        this._nodes = value;
        console.log(value);
    }

    @Output()
    get OnNodeClicked(): EventEmitter<TreeNode> {
        return this._nodeClicked;
    }

    @Output()
    get OnNodeExpanding(): EventEmitter<TreeNode> {
        return this._nodeExpanding;
    }

    OnCaretClicked(node: TreeNode): void {
        node.Toggle();
        this.OnNodeExpanding.emit(node);
    }
}