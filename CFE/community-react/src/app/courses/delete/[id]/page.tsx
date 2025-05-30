"use client";

import Link from "next/link";
import { use, useContext, useEffect, useState } from "react";
import { CourseService } from "@/services/CourseService";
import { ICourse } from "@/types/domain/ICourse";
import { useForm, SubmitHandler, set } from "react-hook-form"
import { useRouter } from 'next/navigation'
import { AccountContext } from "@/context/AccountContext";


export default function CourseDelete({ params }: { params: Promise<{ id: string }> }) {
	const { id } = use(params)
	const { accountInfo, setAccountInfo } = useContext(AccountContext);

	const router = useRouter();
	useEffect(() => {
		if (!accountInfo?.jwt) {
			router.push('/login');
		}
	}, []);

	const [errorMessage, setErrorMessage] = useState("");

	const courseService = new CourseService();

	const [data, setData] = useState<ICourse>();

	const deleteConfirmed = async () => {
		try {
			var result = await courseService.deleteAsync(id);

			console.log('delete result', result)

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


	useEffect(() => {
		if (!accountInfo?.jwt) {
			router.push('/login');
		} else {
			const fetchData = async () => { setData((await courseService.getAsync(id)).data!) };
			fetchData();
		}
	}, []);

	if (!data) {
		return <div>Loading...</div>;
	}

	return (
		<div>
			<h4>Delete Course</h4>
			<h3>Are you sure you want to delete this?</h3>
			<hr />

			{errorMessage.length > 0 && errorMessage}

			<dl className="row">
				<dt className="col-sm-2">
					Name
				</dt>
				<dd className="col-sm-10">
					{data.name}
				</dd>
			</dl>

			<button onClick={() => deleteConfirmed()} type="button" value="Delete" title="Delete" className="btn btn-danger">Delete</button> | <Link href="/courses">Cancel</Link>

		</div>

	);
}
