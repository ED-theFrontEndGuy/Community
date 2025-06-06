import { ITrip } from "@/types/domain/ITrip";

const Trip = (props: ITrip) => {
	return (
		<div className="trips-card-body">
			<h5 className="trips-card-title">{props.name}</h5>
			<p className="trips-card-text">{props.destination}</p>
			<p className="trips-card-text">{props.budget} / {props.tripExpensesTotal}</p>
		</div>
	)
}

export default Trip;
