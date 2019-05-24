import { Component } from '@angular/core';
import { BaseComponent } from '@shared/base.component';
import { ConfigModel } from '@common/index';
import { ConfigService } from '@services/config.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent extends BaseComponent {

  constructor(config: ConfigService) {
    super(config);
  }

  protected async Initialize(): Promise<void> {

  }

  protected async Destroy(): Promise<void> {

  }

  protected OnConfigChanged(config: ConfigModel): void {
    
  }

}
