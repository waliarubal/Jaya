import { Component, ComponentFactoryResolver, ViewChild } from '@angular/core';
import { BaseComponent } from '@shared/base.component';
import { ConfigModel } from '@common/index';
import { ConfigService } from '@services/config.service';
import { PopupService } from '@services/popup.service';
import { WindowModel } from '@shared/window.model';
import { ComponentHostDirective } from '@shared/component-host.directive';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent extends BaseComponent {
  @ViewChild(ComponentHostDirective, { static: false }) private _componentHost: ComponentHostDirective;

  constructor(
    config: ConfigService,
    private readonly _popupService: PopupService,
    private readonly _componentFactoryResolver: ComponentFactoryResolver) {

    super(config);
    this._popupService.OnOpen.subscribe((popupData: WindowModel) => this.OpenPopup(popupData));
    this._popupService.OnClose.subscribe(() => this.ClosePopup());
  }

  get PopupData(): WindowModel {
    return this._popupService.Data;
  }

  get IsPopupOpen(): boolean {
    return this._popupService.IsOpen;
  }

  private OpenPopup(popupData: WindowModel): void {
    const resolver = this._componentFactoryResolver.resolveComponentFactory(popupData.Component);

    const host = this._componentHost.ContainerRef;
    host.clear();
    host.createComponent(resolver);
  }

  private ClosePopup(): void {
    const host = this._componentHost.ContainerRef;
    host.clear();
  }

  protected async Initialize(): Promise<void> {

  }

  protected async Destroy(): Promise<void> {
    this._popupService.OnOpen.unsubscribe();
    this._popupService.OnClose.unsubscribe();
  }

  protected OnConfigChanged(config: ConfigModel): void {

  }

}
