import { IDomainId } from "../IDomainId";

export interface IAssignment extends IDomainId {
	name: string;
	declarationId: string;
}
