"use client";

import Link from "next/link";
import { useEffect, useContext, useState } from "react";
import { AccountContext } from "@/context/AccountContext";
import { CourseService } from "@/services/CourseService"
import { useRouter } from "next/navigation";
import { ICourse } from "@/types/domain/ICourse";

export default function Course() {

	const courseService = new CourseService();
	const { accountInfo } = useContext(AccountContext);
	const [data, setData] = useState<ICourse[]>([]);
	const router = useRouter();

	// -> useEffect runs after every render. Has access to state/prop variables.
	// [] -> only run after the initial render. (Does not run again (except once in development)
	// [a, b] ->  runs after the initial render and after re-renders with changed dependencies. (runs again if a or b are different)
	useEffect(() => {
		if (!accountInfo?.jwt) {
			router.push("/login");
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

	// if (data.length === 0) {
	// 	return <>
	// 		<h1>Courses</h1>

	// 		<p>
	// 			<Link href="/courses/create">Create New course</Link>
	// 		</p>
	// 	</>;
	// }

	return (
		<section>
			ToDo
		</section>
	);
}
