import { BaseService } from '../src-common';

export abstract class SuperService extends BaseService {

    Stop(): void {
        this.Dispose();
    }

    protected abstract Dispose(): void;
}