import { IDomainId } from "../IDomainId";

export interface IStudyGroup extends IDomainId {
	name: string;
	studySessionId: string;
}
