import React from "react";
import { Link } from "react-router-dom";

export function HeaderMain() {
	return (
		<body>
			<header className="header">
				<div className="header__content">
					<div className="content">
						<Link to="/login">
							<button className="button-enter">Войти</button>
						</Link>
					</div>
				</div>
			</header>
		</body>
	);
}
export function HeaderAccount() {
	return (
		<body>
			<header className="header">
				<div className="header__content header-account">
					<div className="content">
						<Link to="">
							<button className="">Главная страница</button>
						</Link>
					</div>
					<div className="content">
						<Link to="">
							<button>Чат</button>
						</Link>
					</div>
					<div className="content">
						<Link to="/login">
							<button>Статистика</button>
						</Link>
					</div>
					<div className="content">
						<Link to="/login">
							<button>Календарь событий</button>
						</Link>
					</div>
					<div className="content">
						<Link to="/login">
							<button>Занятия</button>
						</Link>
					</div>
					<div className="content">
						<Link to="/login">
							<button>Занятия</button>
						</Link>
					</div>
				</div>
			</header>
		</body>
	);
}
