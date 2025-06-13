import { IDomainId } from "../IDomainId";

export interface IDeclaration extends IDomainId {
	active: boolean;
	courseId: string;
	courseName: string;
}
