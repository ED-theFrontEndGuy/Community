"use client";

import Link from 'next/link';
import { useContext } from 'react';
import { AccountContext } from '@/context/AccountContext';
import { useRouter } from 'next/navigation';

export default function Header() {
	const { accountInfo, setAccountInfo } = useContext(AccountContext);
	const router = useRouter();

	return (
		<>{accountInfo?.jwt &&
			<nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
				<div className="container-fluid">
					<Link className="navbar-brand" href="/">WanderFund</Link>
					<button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
						aria-expanded="false" aria-label="Toggle navigation">
						<span className="navbar-toggler-icon"></span>
					</button>
					<div className="navbar-collapse collapse d-sm-inline-flex justify-conten t-between">
						<ul className="navbar-nav flex-grow-1">
							<>
								<li className="nav-item">
									<Link className="nav-link text-dark" href="/expenses">Expenses</Link>
								</li>
							</>

						</ul>

						<ul className="navbar-nav">
							<li className="nav-item">

								{/* {!accountInfo?.jwt &&
								<Link className="nav-link text-dark" href="/login">Login</Link>
							} */}

								{accountInfo?.jwt &&
									<a className="nav-link text-dark" href="#" onClick={() => {
										setAccountInfo!({});
										router.push("/login");
									}}>Logout</a>
								}
							</li>
						</ul>
					</div>
				</div>
			</nav>}
		</>
	);
}
