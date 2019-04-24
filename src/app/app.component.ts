import { Component } from '@angular/core';
import { BaseComponent } from '@shared/base.component';
import { FileSystemService } from '@services/file-system.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  providers: [FileSystemService]
})
export class AppComponent extends BaseComponent {

  constructor(private _fileSystemService: FileSystemService) {
    super();
  }

  ngOnInit(): void {
    this._fileSystemService.GetDirectories('e://');
  }

}
