import { MessageModel } from '@shared/models/message.model';
import { BaseModel } from '@shared/base.model';

export abstract class BaseComponent extends BaseModel {
    
    constructor() {
       super();
    }

    protected SendMessage(message: MessageModel) {
        
    }

    protected abstract ReceiveMessage(message: MessageModel): void;

}