import { IDomainId } from "../IDomainId";

export interface IAttachment extends IDomainId {
	link: string;
	description: string;
	assignmentId: string;
}
