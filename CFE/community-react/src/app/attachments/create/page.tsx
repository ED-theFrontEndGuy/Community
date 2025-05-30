"use client";

import Link from "next/link";
import { useContext, useEffect, useState } from "react";
import { AttachmentService } from "@/services/AttachmentService";
import { IAttachment } from "@/types/domain/IAttachment";
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

	const attachmentService = new AttachmentService();

	type Inputs = {
		link: string;
		description: string;
		assignmentId: string;
	}

	const {
		register,
		handleSubmit,
		formState: { errors },
	} = useForm<Inputs>({});

	const onSubmit: SubmitHandler<Inputs> = async (data) => {
		setErrorMessage("Loading...");
		try {
			var result = await attachmentService.addAsync({ link: data.link, description: data.description, assignmentId: data.assignmentId });
			console.log('create result', result)

			if (result.errors && result.errors.length > 0) {
				setErrorMessage(result.statusCode + " - " + result.errors.join(", "));
				return;
			} else {
				// login was ok, set state and redirect back to main list
				setErrorMessage("");
				router.push('/attachments');
			}

		} catch (error) {
			console.log('error: ', (error as Error).message)
			setErrorMessage((error as Error).message);
		}
	}


	return (
		<>
			<h4>Create Attachments</h4>
			<hr />
			<div className="row">
				<div className="col-md-4">
					<form onSubmit={handleSubmit(onSubmit)}>

						{errorMessage.length > 0 && errorMessage}

						<div className="form-group">
							<label className="control-label" htmlFor="attachmentLink">Link</label>
							<input
								className="form-control"
								type="text"
								id="attachmentLink"
								maxLength={128}
								placeholder="Link"
								{...register("link", { required: true })}
							/>
							{errors.link &&
								<span className="text-danger" >This field is required!</span>
							}

							<label className="control-label" htmlFor="attachmentDescription">Description</label>
							<input
								className="form-control"
								type="text"
								id="attachmentDescription"
								maxLength={128}
								placeholder="Description"
								{...register("description", { required: true })}
							/>

							<label className="control-label" htmlFor="attachmentAssignmentId">AssignmentId</label>
							<input
								className="form-control"
								type="text"
								id="attachmentAssignmentId"
								maxLength={128}
								placeholder="Assignment"
								{...register("assignmentId", { required: true })}
							/>
							{errors.assignmentId &&
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
				<Link href={"/attachments"}>Back to List</Link>
			</div>
		</>
	);
}
