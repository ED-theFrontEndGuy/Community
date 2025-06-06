"use client";

import Link from "next/link";
import { useEffect, useContext, useState } from "react";
import { AccountContext } from "@/context/AccountContext";
import { DeclarationService } from "@/services/DeclarationService"
import { useRouter } from "next/navigation";
import { IDeclaration } from "@/types/domain/IDeclaration";

export default function Course() {

	const declarationService = new DeclarationService();
	const { accountInfo } = useContext(AccountContext);
	const [data, setData] = useState<IDeclaration[]>([]);
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
				const result = await declarationService.getAllAsync();

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

	if (data.length === 0) {
		return <>
			<h1>Declarations</h1>

			<p>You have not declared anything yet</p>
			<p>
				<Link href="/declarations/create">Create New Declaration</Link>
			</p>
		</>;
	}

	return (
		<>
			<h1>Declaration</h1>

			<p>
				<Link href="/declarations/create">Create New</Link>
			</p>
			<table className="table">
				<thead>
					<tr>
						<th>
							Declaration Course
						</th>
						<th>Active</th>
						<th></th>
					</tr>
				</thead>
				<tbody>
					{data.map((declaration) =>
						<tr key={declaration.id}>
							<td>
								{declaration.courseName}
							</td>
							<td>
								{declaration.active ? "Active" : "Inactive"}
							</td>
							<td>
								<Link href={"/declarations/edit/" + declaration.id}> Edit </Link> |
								<Link href={"/declarations/delete/" + declaration.id}> Delete </Link>
							</td>
						</tr>
					)}
				</tbody>
			</table>

		</>
	);
}
