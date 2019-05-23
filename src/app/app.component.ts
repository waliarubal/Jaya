import { Component } from '@angular/core';
import { BaseComponent } from '@shared/base.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent extends BaseComponent {

  protected async Initialize(): Promise<void> {

  }

  protected async Destroy(): Promise<void> {

  }
  
}
