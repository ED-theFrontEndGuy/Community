"use client";

import Link from 'next/link';
import { useContext } from 'react';
import { AccountContext } from '@/context/AccountContext';

export default function Header() {
	const { accountInfo, setAccountInfo } = useContext(AccountContext);
	return (
		<nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
			<div className="container-fluid">
				<Link className="navbar-brand" href="/">Community</Link>
				<button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
					aria-expanded="false" aria-label="Toggle navigation">
					<span className="navbar-toggler-icon"></span>
				</button>
				<div className="navbar-collapse collapse d-sm-inline-flex justify-conten t-between">
					<ul className="navbar-nav flex-grow-1">
						<li className="nav-item">
							<Link className="nav-link text-dark" href="/">Home</Link>
						</li>
                        <li className="nav-item">
                            <Link className="nav-link text-dark" href="/declaration">Declarations</Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link text-dark" href="/courses">Courses</Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link text-dark" href="/timelog">Timelogs</Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link text-dark" href="/attachment">Attachments</Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link text-dark" href="/assignment">Assignments</Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link text-dark" href="/room">Rooms</Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link text-dark" href="/studysession">StudySessions</Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link text-dark" href="/studygroup">StudyGroups</Link>
                        </li>
					</ul>

					<ul className="navbar-nav">
						<li className="nav-item">

						{ !accountInfo?.jwt &&
                            <Link className="nav-link text-dark" href="/login">Login</Link>
						}

						{ accountInfo?.jwt &&
                            <Link className="nav-link text-dark" href="/login">Logout</Link>
						}

                        </li>
					</ul>
				</div>
			</div>
		</nav>
	);
}
