"use client";

import { AccountContext } from "@/context/AccountContext";
import { useRouter } from "next/navigation";
import { useContext, useEffect } from "react";

export default function Home() {
	const { accountInfo, setAccountInfo } = useContext(AccountContext);
	const router = useRouter();

	useEffect(() => {
        if (!accountInfo?.jwt) {
            router.push("/login");
        }
    }, [accountInfo, router]);

    if (!accountInfo?.jwt) {
        return null; // Or a loading spinner
    }

	return (
		<>
			Dashboard
		</>
	);
}
