export interface IExpenseAdd {
	name: string;
	expenseReference?: string;
	expenseCost?: number;
	currency?: string;
	expenseCategoryId: string;
}
