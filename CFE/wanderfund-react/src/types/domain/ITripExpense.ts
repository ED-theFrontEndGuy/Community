import { IDomainId } from "../IDomainId";

export interface ITripExpense extends IDomainId {
	tripId: string;
	expenseId: string;
}
