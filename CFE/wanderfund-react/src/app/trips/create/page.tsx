"use client";

import Link from "next/link";
import { useContext, useEffect, useState } from "react";
import { ITrip } from "@/types/domain/ITrip";
import { useForm, SubmitHandler, set } from "react-hook-form"
import { useRouter } from 'next/navigation'
import { AccountContext } from "@/context/AccountContext";
import { TripService } from "@/services/TripService";
import { Timestamp } from "next/dist/server/lib/cache-handlers/types";

export default function Coursecreate() {
	const { accountInfo, setAccountInfo } = useContext(AccountContext);
	const router = useRouter();
	useEffect(() => {
		if (!accountInfo?.jwt) {
			router.push('/login');
		}
	}, []);

	const [errorMessage, setErrorMessage] = useState("");

	const tripService = new TripService();

	type Inputs = {
		name: string;
		destination?: string;
		budget?: number;
		departureDate?: Timestamp;
		returnDate?: Timestamp;
		isPublic?: boolean;
		tripExpensesTotal?: number;
	}

	const {
		register,
		handleSubmit,
		formState: { errors },
	} = useForm<Inputs>({});

	const onSubmit: SubmitHandler<Inputs> = async (data) => {
		setErrorMessage("Loading...");
		try {
			const result = await tripService.addAsync({
				name: data.name,
				destination: data.destination,
				budget: data.budget,
				departureDate: data.departureDate,
				returnDate: data.returnDate,
				isPublic: data.isPublic
			});

			console.log('create result', result)

			if (result.errors && result.errors.length > 0) {
				setErrorMessage(result.statusCode + " - " + result.errors.join(", "));
				return;
			} else {
				// login was ok, set state and redirect back to main list
				setErrorMessage("");
				router.push('/');
			}

		} catch (error) {
			console.log('error: ', (error as Error).message)
			setErrorMessage((error as Error).message);
		}
	}


	return (
		<>
			<h4>Create new Trip</h4>
			<hr />
			<div className="row">
				<div className="col-md-4">
					<form onSubmit={handleSubmit(onSubmit)}>

						{errorMessage.length > 0 && errorMessage}

						<div className="form-group">
							<label className="control-label" htmlFor="tripName">Name</label>
							<input
								className="form-control"
								type="text"
								id="tripName"
								maxLength={128}
								placeholder="Name"
								{...register("name", { required: true })}
							/>
							{errors.name &&
								<span className="text-danger" >This field is required!</span>
							}

							<label className="control-label" htmlFor="tripDestination">Destination</label>
							<input
								className="form-control"
								type="text"
								id="tripDestination"
								maxLength={128}
								placeholder="Name"
								{...register("destination", { required: false })}
							/>

							<label className="control-label" htmlFor="tripDestination">Destination</label>
							<input
								className="form-control"
								type="text"
								id="tripDestination"
								maxLength={128}
								placeholder="Name"
								{...register("destination", { required: false })}
							/>

							<label className="control-label" htmlFor="tripBudget">Budget</label>
							<input
								className="form-control"
								type="number"
								id="tripBudget"
								maxLength={128}
								placeholder="Name"
								{...register("budget", { required: false })}
							/>

							<label className="control-label" htmlFor="departureDate">Departure</label>
							<input
								className="form-control"
								type="datetime-local"
								id="departureDate"
								{...register("departureDate", { required: false })}
							/>

							<label className="control-label" htmlFor="returnDate">Return Date</label>
							<input
								className="form-control"
								type="datetime-local"
								id="returnDate"
								{...register("returnDate", { required: false })}
							/>

							<label className="control-label" htmlFor="isPublic">Make the trip public?</label>
							<select
								className="form-control"
								id="isPublic"
								{...register("isPublic", { required: false })}
							>
								<option value="">Select...</option>
								<option value="true">Yes</option>
								<option value="false">No</option>
							</select>
						</div>

						<div className="form-group">
							<input type="submit" value="Create" className="btn btn-primary" />
						</div>
					</form>
				</div>
			</div>

			<div>
				<Link href={"/"}>Back to List</Link>
			</div>
		</>
	);
}
