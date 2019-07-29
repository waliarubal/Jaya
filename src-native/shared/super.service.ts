import { BaseService } from '../../src-common';

export abstract class SuperService extends BaseService {

    async Stop(): Promise<void> {
        await this.Dispose();
    }

    async Start(): Promise<void> {
        await this.Initialize();
    }

    protected async abstract Dispose(): Promise<void>;

    protected async abstract Initialize(): Promise<void>;
}