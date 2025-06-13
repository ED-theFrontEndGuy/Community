"use client";

import Link from "next/link";
import { useContext, useEffect, useState } from "react";
import { CourseService } from "@/services/CourseService";
import { ICourse } from "@/types/domain/ICourse";
import { useForm, SubmitHandler, set } from "react-hook-form"
import { useRouter } from 'next/navigation'
import { AccountContext } from "@/context/AccountContext";

export default function Coursecreate() {
	const { accountInfo, setAccountInfo } = useContext(AccountContext);
	const router = useRouter();
	useEffect(() => {
		if (!accountInfo?.jwt) {
			router.push('/login');
		}
	}, []);

	const [errorMessage, setErrorMessage] = useState("");

	const courseService = new CourseService();

	type Inputs = {
		name: string
	}

	const {
		register,
		handleSubmit,
		formState: { errors },
	} = useForm<Inputs>({});

	const onSubmit: SubmitHandler<Inputs> = async (data) => {
		setErrorMessage("Loading...");
		try {
			var result = await courseService.addAsync({ name: data.name });
			console.log('create result', result)

			if (result.errors && result.errors.length > 0) {
				setErrorMessage(result.statusCode + " - " + result.errors.join(", "));
				return;
			} else {
				// login was ok, set state and redirect back to main list
				setErrorMessage("");
				router.push('/courses');
			}

		} catch (error) {
			console.log('error: ', (error as Error).message)
			setErrorMessage((error as Error).message);
		}
	}


	return (
		<>
			CourseCreate
			<h4>Create Courses</h4>
			<hr />
			<div className="row">
				<div className="col-md-4">
					<form onSubmit={handleSubmit(onSubmit)}>

						{errorMessage.length > 0 && errorMessage}

						<div className="form-group">
							<label className="control-label" htmlFor="courseName">Name</label>
							<input
								className="form-control"
								type="text"
								id="courseName"
								maxLength={128}
								placeholder="Name"
								{...register("name", { required: true })}
							/>
							{errors.name &&
								<span className="text-danger" >This field is required!</span>
							}

						</div>

						<div className="form-group">
							<input type="submit" value="Create" className="btn btn-primary" />
						</div>
					</form>
				</div>
			</div>

			<div>
				<Link href={"/courses"}>Back to List</Link>
			</div>
		</>
	);
}
