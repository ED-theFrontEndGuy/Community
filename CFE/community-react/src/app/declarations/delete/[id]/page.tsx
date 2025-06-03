"use client";

import Link from "next/link";
import { use, useContext, useEffect, useState } from "react";
import { DeclarationService } from "@/services/DeclarationService";
import { IDeclaration } from "@/types/domain/IDeclaration";
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

	const declarationService = new DeclarationService();

	const [declaration, setDeclaration] = useState<IDeclaration>();

	const deleteConfirmed = async () => {
		try {
			var result = await declarationService.deleteAsync(id);

			console.log('delete result', result)

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


	useEffect(() => {
		if (!accountInfo?.jwt) {
			router.push('/login');
		} else {
			const fetchData = async () => { setDeclaration((await declarationService.getAsync(id)).data!) };
			fetchData();
		}
	}, []);

	if (!declaration) {
		return <div>Loading...</div>;
	}

	return (
		<div>
			<h4>Delete Declaration</h4>
			<h3>Are you sure you want to delete this?</h3>
			<hr />

			{errorMessage.length > 0 && errorMessage}

			<dl className="row">
				<dt className="col-2">
					Name
				</dt>
				<dt className="col-10">
					Status
				</dt>
				<dd className="col-2">
					{declaration.courseName}
				</dd>
				<dd className="col-10">
					{declaration.active ? "active" : "inactive"}
				</dd>
			</dl>

			<button onClick={() => deleteConfirmed()} type="button" value="Delete" title="Delete" className="btn btn-danger">Delete</button> | <Link href="/declarations">Cancel</Link>

		</div>

	);
}
