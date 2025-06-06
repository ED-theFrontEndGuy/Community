import { IDomainId } from "../IDomainId";

export interface ITrip extends IDomainId {
	name: string;
	destination?: string;
	budget?: number;
	departureDate?: string;
	returnDate?: string;
	isPublic?: boolean;
	tripExpensesTotal?: number;
}
