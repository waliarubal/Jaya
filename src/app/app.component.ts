import { Component } from '@angular/core';
import { IpcService } from '@services/ipc.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  providers: [IpcService]
})
export class AppComponent {

}
