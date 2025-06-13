import { IAttachment } from "@/types/domain/IAttachment";
import { EntityService } from "./EntityService";
import { IAttachmentAdd } from "@/types/domain/IAttachmentAdd";


export class AttachmentService extends EntityService<IAttachment, IAttachmentAdd> {
	constructor() {
		super('attachments');
	}
}
