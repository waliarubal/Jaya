import { Directive, ViewContainerRef } from '@angular/core';

@Directive({
    selector: '[component-host]'
})
export class ComponentHostDirective {

    constructor(private readonly _containerRef: ViewContainerRef) { }

    get ContainerRef(): ViewContainerRef {
        return this._containerRef;
    }
}