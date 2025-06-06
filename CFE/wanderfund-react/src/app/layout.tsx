"use client";

import 'bootstrap/dist/css/bootstrap.css';
import './globals.css';
import BootstrapActivation from '@/helpers/BootstrapActivation';
import Header from "@/components/Header";
import { AccountContext, IAccountInfo } from "@/context/AccountContext";
import { useState } from "react";


export default function RootLayout({
	children,
}: Readonly<{
	children: React.ReactNode;
}>) {
	const [accountInfo, setAccountInfo] = useState<IAccountInfo | undefined>();

	const updateAccountInfo = (value: IAccountInfo) => {
		setAccountInfo(value);
		localStorage.setItem("_jwt", value.jwt!);
		localStorage.setItem("_refreshToken", value.refreshToken!);
	}

	return (
		<html lang="en">
			<body>
				<div className={!accountInfo?.jwt ? "content" : ""}>
					<AccountContext.Provider value={{
						accountInfo: accountInfo,
						setAccountInfo: updateAccountInfo,
					}}>
						<Header />
						<div className="container">
							<main role="main" className="pb-3">
								{children}
							</main>
						</div>
						<BootstrapActivation />
					</AccountContext.Provider>
				</div>
			</body>
		</html>
	);
}
