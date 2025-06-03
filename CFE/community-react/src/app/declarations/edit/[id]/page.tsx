"use client";

import Link from "next/link";
import { use, useContext, useEffect, useState } from "react";
import { DeclarationService } from "@/services/DeclarationService";
import { useForm, SubmitHandler, set } from "react-hook-form"
import { useRouter } from 'next/navigation'
import { AccountContext } from "@/context/AccountContext";
import { CourseService } from "@/services/CourseService";
import { ICourse } from "@/types/domain/ICourse";


export default function CourseEdit({ params }: { params: Promise<{ id: string }> }) {
	const { id } = use(params);
	const { accountInfo, setAccountInfo } = useContext(AccountContext);
	const [errorMessage, setErrorMessage] = useState("");
	const [course, setCourse] = useState<ICourse[]>([]);
	const declarationService = new DeclarationService();
	const courseService = new CourseService();

	console.log("Params for edit: " + params);


	const router = useRouter();
	useEffect(() => {
		if (!accountInfo?.jwt) {
			router.push('/login');
		}

		async function fetchCourses() {
			const response = await courseService.getAllAsync();
			setCourse(response.data!);
		}

		fetchCourses();
	}, []);

	type Inputs = {
		active: boolean;
		courseId: string;
		courseName: string;
	}

	const {
		register,
		handleSubmit,
		formState: { errors },
	} = useForm<Inputs>({
		defaultValues: async () => {
			const result = await declarationService.getAsync(id);
			if (result.errors && result.errors.length > 0) {
				setErrorMessage(result.statusCode + " - " + result.errors.join(", "));

				return { active: false, courseId: "", courseName: "" };
			} else {
				setErrorMessage("");

				return { active: result.data!.active, courseId: result.data!.courseId, courseName: result.data!.courseName };
			}
		}
	});

	const onSubmit: SubmitHandler<Inputs> = async (data) => {
		setErrorMessage("Loading...");
		try {
			var result = await declarationService.updateAsync({ id: id, active: data.active, courseId: data.courseId, courseName: data.courseName });
			console.log('edit result', result)

			if (result.errors && result.errors.length > 0) {
				setErrorMessage(result.statusCode + " - " + result.errors.join(", "));
				return;
			} else {
				// login was ok, set state and redirect back to main list
				setErrorMessage("");
				router.push('/declarations');
			}

		} catch (error) {
			console.log('error: ', (error as Error).message)
			setErrorMessage((error as Error).message);
		}
	}

	return (
		<>
			<h4>Edit Declaration</h4>
			<hr />
			<div className="row">
				<div className="col-md-4">
					<form onSubmit={handleSubmit(onSubmit)}>

						{errorMessage.length > 0 && errorMessage}

						<div className="form-group">
							<label className="control-label">Active</label>
							<div>
								<label>
									<input
										type="checkbox"
										{...register("active")}
									/>
								</label>
							</div>
						</div>

						<div className="form-group">
							<label className="control-label" htmlFor="courseId">Course</label>
							<select
								className="form-control"
								id="courseId"
								{...register("courseId", { required: true })}
							>
								<option value="">Select course</option>
								{course.map(course => (
									<option key={course.id} value={course.id}>
										{course.name}
									</option>
								))}
							</select>
							{errors.courseId &&
								<span className="text-danger">This field is required!</span>
							}
						</div>

						<div className="form-group">
							<input type="submit" value="Create" className="btn btn-primary" />
						</div>
					</form>
				</div>
			</div>

			<div>
				<Link href={"/declarations"}>Back to List</Link>
			</div>
		</>
	);
}
