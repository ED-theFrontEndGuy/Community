"use client";

import { useForm, SubmitHandler } from "react-hook-form"
import { useContext, useState } from "react";
import { useRouter } from "next/navigation";
import { AccountService } from "@/services/AccountService";
import { AccountContext } from "@/context/AccountContext";

export default function Register() {
	const accountService = new AccountService();
	const { setAccountInfo } = useContext(AccountContext);
	const [errorMessage, setErrorMessage] = useState("");
	const router = useRouter();

	type Inputs = {
		email: string;
		password: string;
		firstName: string;
		lastName: string;
	}

	const {
		register,
		handleSubmit,
		formState: { errors }
	} = useForm<Inputs>({
		defaultValues: {
			email: "",
			password: "",
			firstName: "",
			lastName: "",
		}
	});

	const onSubmit: SubmitHandler<Inputs> = async (data: Inputs) => {
		console.log(data);
		setErrorMessage("Loading...");

		try {
			var result = await accountService.registerAsync(data.email, data.firstName, data.lastName, data.password);

			if (result.errors) {
				setErrorMessage(result.statusCode + " " + result.errors[0]);
				return;
			}

			setErrorMessage("");

			setAccountInfo!({
				jwt: result.data!.jwt,
				refreshToken: result.data!.refreshToken
			});

			router.push("/");
		} catch (error) {
			setErrorMessage("Login failed - " + (error as Error).message);
		}
	};

	return (
		<div className="row">
			<div className="col-4"></div>
			<div className="col-4">

				{errorMessage}

				<form onSubmit={handleSubmit(onSubmit)}>
					<h2>Register</h2>
					<hr />
					<div asp-validation-summary="ModelOnly" className="text-danger" role="alert"></div>

					<div className="form-floating mb-3">
						<input
							className="form-control"
							aria-required="true"
							placeholder="name@example.com"
							type="email"
							id="Input_Email"
							{...register("email", { required: true })}
						/>
						<label className="form-label" htmlFor="Input_Email">Email</label>
						{
							errors.email &&
							<span className="text-danger">Email is required</span>
						}

					</div>

					<div className="form-floating mb-3">
						<input
							className="form-control"
							aria-required="true"
							placeholder="First name"
							type="text"
							id="Input_FirstName"
							{...register("firstName", { required: true })}
						/>
						<label className="form-label" htmlFor="Input_FirstName">Name</label>
						{
							errors.firstName &&
							<span className="text-danger">Required</span>
						}

					</div>

					<div className="form-floating mb-3">
						<input
							className="form-control"
							aria-required="true"
							placeholder="Last name"
							type="text"
							id="Input_LastName"
							{...register("lastName", { required: true })}
						/>
						<label className="form-label" htmlFor="Input_LastName">Surname</label>
						{
							errors.lastName &&
							<span className="text-danger">Required</span>
						}

					</div>

					<div className="form-floating mb-3">
						<input
							className="form-control"
							aria-required="true"
							placeholder="password"
							type="password"
							id="Input_Password"
							{...register("password", { required: true })}
						/>
						<label className="form-label" htmlFor="Input_Password">Password</label>
						{
							errors.password &&
							<span className="text-danger">Password is required</span>
						}
					</div>
					<div>
						<button id="register-submit" type="submit" className="w-100 btn btn-lg btn-primary">Register</button>
					</div>
				</form>
			</div>
		</div>
	);
}
