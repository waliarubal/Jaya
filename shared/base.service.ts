import { BaseModel } from '@shared/base.model';
import { Constants } from '@shared/constants';

export abstract class BaseService extends BaseModel {
    protected readonly IPC_CHANNEL: string = Constants.IPC_CHANNEL;
}