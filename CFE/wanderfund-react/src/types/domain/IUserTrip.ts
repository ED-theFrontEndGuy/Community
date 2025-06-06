import { IDomainId } from "../IDomainId";

export interface IUserTrip extends IDomainId {
	isUserTripAdmin?: boolean;
	tripId: string;
}
