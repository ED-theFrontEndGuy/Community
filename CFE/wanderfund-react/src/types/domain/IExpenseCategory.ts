import { IDomainId } from "../IDomainId";

export interface IexpenseCategory extends IDomainId {
	name: string;
	description?: string;
}
