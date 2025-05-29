"use client";

import { useForm, SubmitHandler } from "react-hook-form"
import { useState } from "react";
import { useRouter } from "next/router";
import { AccountService } from "@/services/AccountService";

export default function Login() {
	const accountService = new AccountService();

	type Inputs = {
		email: string;
		password: string;
	}

	const {
		register,
		handleSubmit,
		formState: { errors }
	} = useForm<Inputs>({
		defaultValues: {
			email: "",
			password: ""
		}
	});

	const onSubmit: SubmitHandler<Inputs> = async (data: Inputs) => {
		console.log(data);
		setErrorMessage("Loading...");

		try {
			var result = await accountService.loginAsync(data.email, data.password);

			if (result.errors) {
				setErrorMessage(result.statusCode + " " + result.errors[0]);
				return;
			}

			setErrorMessage(JSON.stringify(result.data));

			// TODO: save jwt

			// TODO: navigate to home

		} catch (error) {
			setErrorMessage("Login failed - " + (error as Error).message);
		}
	};

	const [errorMessage, setErrorMessage] = useState("");
	// const router = useRouter();

	return (
		<div className="row">
			<div className="col-4"></div>
			<div className="col-4">

				{ errorMessage }
				
				<form onSubmit={handleSubmit(onSubmit)}>
					<h2>Login</h2>
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
						<button id="login-submit" type="submit" className="w-100 btn btn-lg btn-primary">Log in</button>
					</div>
				</form>
			</div>
		</div>
	);
}
