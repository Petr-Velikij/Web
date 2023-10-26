import React, { useEffect } from "react";
import { Link } from "react-router-dom";

import img_back_button from "../styles/img/back-button.svg";

import "../styles/css/login.css";

function Login() {
	const color = "#E8E7E7";
	useEffect(() => {
		document.body.style.backgroundColor = color;
	});

	return (
		<body className="login">
			<div className="login-container__content">
				<div className="content__button-back">
					<Link to="/">
						<button className="button-back">
							<img src={img_back_button} alt="Oops" />
						</button>
					</Link>
				</div>
				<div className="content__title">Вход</div>
				<div className="content__form-item">
					<div className="form-item">
						<input type="text" placeholder="Логин"></input>
					</div>
					<div className="form-item">
						<input type="password" placeholder="Пароль"></input>
					</div>
					<div className="form-item">
						<button className="button-next">Продолжить</button>
					</div>
				</div>
				<div className="content__text">
					<Link className="text">Забыли пароль?</Link>
					<div className="text">
						Не учетной записи?{" "}
						<Link to="/register" className="text">
							Зарегистрируйтесь
						</Link>
					</div>
				</div>
			</div>
		</body>
	);
}
export default Login;
