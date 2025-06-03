"use client";

import Link from "next/link";
import { useContext, useEffect, useState } from "react";
import { DeclarationService } from "@/services/DeclarationService";
import { IDeclaration } from "@/types/domain/IDeclaration";
import { useForm, SubmitHandler, set } from "react-hook-form"
import { useRouter } from 'next/navigation'
import { AccountContext } from "@/context/AccountContext";
import { CourseService } from "@/services/CourseService";
import { ICourse } from "@/types/domain/ICourse";

export default function DeclarationCreate() {
	const { accountInfo, setAccountInfo } = useContext(AccountContext);
	const [course, setData] = useState<ICourse[]>([]);
	const router = useRouter();
	useEffect(() => {
		if (!accountInfo?.jwt) {
			router.push('/login');
		}

		const fetchData = async () => {
			try {
				const result = await courseService.getAllAsync();

				if (result.errors) {
					console.log(result.errors);
					return;
				}

				setData(result.data!);
			} catch (error) {
				console.log("Error fetching data: ", error);
			}
		};

		fetchData();
	}, []);

	const [errorMessage, setErrorMessage] = useState("");

	const declarationService = new DeclarationService();
	const courseService = new CourseService();

	type Inputs = {
		active: boolean;
		courseId: string;
	}

	const {
		register,
		handleSubmit,
		formState: { errors },
	} = useForm<Inputs>({});

	const onSubmit: SubmitHandler<Inputs> = async (data) => {
		setErrorMessage("Loading...");
		try {
			var result = await declarationService.addAsync({ active: data.active, courseId: data.courseId });
			console.log('create result', result)

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
			<h4>Create Declaration</h4>
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
                                        value="false"
										defaultChecked={false}
                                        {...register("active")}
                                    />
                                </label>

                            </div>
                            {errors.active &&
                                <span className="text-danger">This field is required!</span>
                            }
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
