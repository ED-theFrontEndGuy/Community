
export interface ITripAdd {
	name: string;
	destination?: string;
	budget?: number;
	departureDate?: string;
	returnDate?: string;
	isPublic?: boolean;
	tripExpenseTotal?: number;
}
