import { ElectronService } from 'ngx-electron';

export abstract class BaseComponent {
    private readonly _electronService: ElectronService;

    constructor(electronService: ElectronService) {
        this._electronService = electronService;
    }

    protected get ElectronService(): ElectronService {
        return this._electronService;
    }

}