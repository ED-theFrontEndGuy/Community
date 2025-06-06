"use client";

import Trip from "@/components/Trip";
import { AccountContext } from "@/context/AccountContext";
import { TripService } from "@/services/TripService";
import { useRouter } from "next/navigation";
import { useContext, useState, useEffect } from "react";

export default function Home() {
	const { accountInfo, setAccountInfo } = useContext(AccountContext);
	const router = useRouter();
	const [trips, setTrips] = useState<ITrip[]>([]);

	useEffect(() => {
		if (!accountInfo?.jwt) {
			router.push("/login");
		}

		const tripService = new TripService();

		// Pass setAccountInfo when needed
		// tripService.updateAccountInfo({ name: "Alice" }, setAccountInfo);

		const fetchTrips = async () => {
			try {
				const result = await tripService.getAllAsync();

				if (result.errors) {
					console.log(result.errors);
					return;
				}

				setTrips(result.data!);
			} catch (error) {
				console.log("Error fetching trips: ", error);
			}
		};

		fetchTrips();

	}, [accountInfo, router, setAccountInfo]);

	if (!accountInfo?.jwt) {
		return null; // Or a loading spinner
	}

	return (
		<>
			<div className="trips-container">
				<div className="trips-card-body">

				</div>
				{trips.map((trip) => (
					<Trip key={trip.id} id={trip.id} name={trip.name} destination={trip.destination} budget={trip.budget} tripExpensesTotal={trip.tripExpensesTotal} />
				))}
			</div>

		</>
	);
}
