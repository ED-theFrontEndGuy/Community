import { IAttachment } from "@/types/domain/IAttachment";
import { EntityService } from "./EntityService";
import { IAttachmentAdd } from "@/types/DTOs/IAttachmentAdd";


export class AttachmentService extends EntityService<IAttachment, IAttachmentAdd> {
	constructor() {
		super('attachments');
	}
}
