import { BaseModel } from '@shared/base.model';

export abstract class BaseService extends BaseModel {
    protected abstract Dispose(): void;
}