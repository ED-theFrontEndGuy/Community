import { Timestamp } from "next/dist/server/lib/cache-handlers/types";
import { IDomainId } from "../IDomainId";

export interface ITrip extends IDomainId {
	name: string;
	destination?: string;
	budget?: number;
	departureDate?: Timestamp;
	returnDate?: Timestamp;
	isPublic?: boolean;
	tripExpensesTotal?: number;
}
