export class TreeNode {
    private _isExpanded: boolean;

    constructor() {
        this._isExpanded = false;
    }

    Label: string;
    Icon: string;
    Data: any;
    Children: TreeNode[];
    IsLeaf: boolean;

    get IsExpanded(): boolean {
        return this._isExpanded;
    }

    Toggle(): void {
        this._isExpanded = !this._isExpanded;
    }
}