import { ITrip } from "@/types/domain/ITrip";
import { useRouter } from "next/navigation";

const Trip = (props: ITrip) => {
	const router = useRouter();
	return (
		<div className="trips-card-body">
			<h5
				className="trips-card-title"
				style={{ cursor: "pointer" }}
				onClick={() => router.push(`/tripexpenses/${props.id}`)}
			>
				{props.name}
			</h5>
			<p className="trips-card-text">{props.destination}</p>
			<p className="trips-card-text">{props.budget} / {props.tripExpensesTotal}</p>
		</div>
	)
}

export default Trip;
