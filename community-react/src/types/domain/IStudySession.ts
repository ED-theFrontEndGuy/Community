import { IDomainId } from "../IDomainId";

export interface IStudySession extends IDomainId {
	description: string;
	active: boolean;
	assignmentId: string;
	assignmentName: string;
	roomId: string;
	roomName: string;
}
