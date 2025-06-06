import { IDomainId } from "../IDomainId";

export interface IExpense extends IDomainId {
	name: string;
	expenseReference?: string;
	expenseCost?: number;
	currency?: string;
	expenseCategoryId: string;
}
