import { Timestamp } from "next/dist/server/lib/cache-handlers/types";

export interface ITripAdd {
	name: string;
	destination?: string;
	budget?: number;
	departureDate?: Timestamp;
	returnDate?: Timestamp;
	isPublic?: boolean;
	tripExpenseTotal?: number;
}
