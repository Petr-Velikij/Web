import React, { Component } from "react";
import { Link } from "react-router-dom";
import axios from "axios";

import "../styles/css/main.css";

export function HeaderMain() {
	const GetData = async () => {
		const urlServer = "https://25.81.29.249:7045/api/persons/";

		await axios.get(urlServer).then((my_data) => console.log(my_data.data));
	};
	return (
		<header className="header">
			<div className="header__content">
				<div className="header__items">
					<div>Lorem</div>
					<div>Lorem</div>
					<div>
						<button onClick={GetData}>Проверка</button>
					</div>
					<div>
						<Link to="/login">
							<button className="button">Войти</button>
						</Link>
					</div>
				</div>
			</div>
		</header>
	);
}
export function HeaderAccount() {
	return (
		<header className="header">
			<div className="header__content">
				<div className="header__items header-account__items">
					<div>
						<Link to="">
							<button className="button">Главная страница</button>
						</Link>
					</div>
					<div>
						<Link to="">
							<button className="button">Чат</button>
						</Link>
					</div>
					<div>
						<Link to="/login">
							<button className="button">Статистика</button>
						</Link>
					</div>
					<div>
						<Link to="/login">
							<button className="button">Календарь событий</button>
						</Link>
					</div>
					<div>
						<Link to="/login">
							<button className="button">Занятия</button>
						</Link>
					</div>
					<div>
						<Link to="/login">
							<button className="button">Занятия</button>
						</Link>
					</div>
				</div>
			</div>
		</header>
	);
}
