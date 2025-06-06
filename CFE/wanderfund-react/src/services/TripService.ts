import { ITrip } from "@/types/domain/ITrip";
import { EntityService } from "./EntityService";
import { ITripAdd } from "@/types/domain/ITripAdd";


export class TripService extends EntityService<ITrip, ITripAdd> {
	constructor() {
		super('trips');
	}
}
