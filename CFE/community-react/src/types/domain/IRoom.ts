import { IDomainId } from "../IDomainId";

export interface IRoom extends IDomainId {
	name: string;
	description: string;
}
