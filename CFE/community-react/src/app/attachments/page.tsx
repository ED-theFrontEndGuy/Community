"use client";

import Link from "next/link";
import { useEffect, useContext, useState } from "react";
import { AccountContext } from "@/context/AccountContext";
import { AttachmentService } from "@/services/AttachmentService"
import { useRouter } from "next/navigation";
import { IAttachment } from "@/types/domain/IAttachment";

export default function Attachment() {

	const attachmentService = new AttachmentService();
	const { accountInfo } = useContext(AccountContext);
	const [data, setData] = useState<IAttachment[]>([]);
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
				const result = await attachmentService.getAllAsync();
				console.log("result", result);


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
			<h1>Attachments</h1>

			<p>
				<Link href="/attachments/create">Create New attachment</Link>
			</p>
		</>;
	}

	return (
		<>
			<h1>Attachments</h1>

			<p>
				<Link href="/attachments/create">Create New attachment</Link>
			</p>
			<table className="table">
				<thead>
					<tr>
						<th>
							Link
						</th>
						<th>
							Description
						</th>
						<th></th>
					</tr>
				</thead>
				<tbody>
					{data.map((attachment) =>
						<tr key={attachment.id}>
							<td>
								{attachment.link}
							</td>
							<td>
								{attachment.description}
							</td>
							<td>
								<Link href={"/attachments/edit/" + attachment.id}> Edit </Link> |
								<Link href={"/attachments/delete/" + attachment.id}> Delete </Link>
							</td>
						</tr>
					)}
				</tbody>
			</table>

		</>
	);
}
