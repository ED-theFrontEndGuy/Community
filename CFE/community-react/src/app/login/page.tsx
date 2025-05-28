"use client";

import { useForm, SubmitHandler } from "react-hook-form"

export default function Login() {

	type Inputs = {
		email: string;
		password: string;
	}

	const {
		register,
		handleSubmit,
		formState: {
			errors
		}
	} = useForm<Inputs>({
		defaultValues: {
			email: "",
			password: ""
		}
	});

	const onSubmit: SubmitHandler<Inputs> = async (data: Inputs) => {
		console.log(data);
	};

	const onError = (errors: any) => {
		console.log(errors);
	};

	return (
		<div className="row">
			<div className="col-4"></div>
			<div className="col-4">
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
						<span className="text-danger"></span>
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
						<span className="text-danger"></span>
					</div>
					<div>
						<button id="login-submit" type="submit" className="w-100 btn btn-lg btn-primary">Log in</button>
					</div>
				</form>
			</div>
		</div>
	);
}
