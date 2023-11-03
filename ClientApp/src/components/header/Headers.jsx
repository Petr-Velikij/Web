import React, { useState } from "react";
import { Link } from "react-router-dom";
import Menu from "../burger/Menu";

export function HeaderMain() {
	return (
		<header className="header">
			<div className="header__content">
				<div>
					<Link to="/login">
						<button className="button-enter">Войти</button>
					</Link>
				</div>
			</div>
		</header>
	);
}
export function HeaderAccount() {
	const [burgerActive, setBurgerActive] = useState(false);

	return (
		<header className="header">
			<div className="header__content -header-account">
				<ul className="content__list-item">
					<li className="list-item">
						<Link to="/" className="button-list">
							Главная страница
						</Link>
					</li>
					<li className="list-item">
						<Link to="/" className="button-list">
							Чат
						</Link>
					</li>
					<li className="list-item">
						<Link to="/add" className="button-list">
							Добавить занятие
						</Link>
					</li>
					<li className="list-item">
						<Link to="/" className="button-list">
							Календарь занятий
						</Link>
					</li>
				</ul>
			</div>
		</header>
	);
}
