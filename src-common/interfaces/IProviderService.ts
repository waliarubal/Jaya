import { ProviderModel, ProviderType } from '../models/provider.model';
import { DirectoryModel } from '../models/directory.model';
import { BaseService } from '../base.service';

export interface IProviderService extends BaseService {
    readonly Type: ProviderType;
    GetProvider(): Promise<ProviderModel>;
    GetDirectory(path: string): Promise<DirectoryModel>;
}