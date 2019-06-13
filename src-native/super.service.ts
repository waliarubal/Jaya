import { BaseService } from '../src-common';

export abstract class SuperService extends BaseService {

    async Stop(): Promise<void> {
        await this.Dispose();
    }

    protected async abstract Dispose(): Promise<void>;
}