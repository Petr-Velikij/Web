import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import axios from "axios";
import { useForm, SubmitHandler, set } from "react-hook-form";

import img_back_button from "../styles/img/back-button.svg";

import "../styles/css/register.css";
import { event } from "jquery";

function Register() {
	const [email, setEmail] = useState();
	const [password, setPassword] = useState();

	const color = "#E8E7E7";
	useEffect(() => {
		document.body.style.backgroundColor = color;
	});

	const {
		register,
		handleSubmit,
		formState: { errors },
	} = useForm();

	const onSubmit = async (Event) => {
		const urlServer = "https://25.81.29.249:7045/api/persons";

		await axios
			.post(urlServer, {
				Name: email,
				Password: password,
			})
			.then((DataSQL) => {
				console.log(DataSQL.data);
			})
			.catch((DataSQL) => {
				console.log();
			});
	};
	return (
		<body className="register">
			<div className="register-container__content">
				<div className="content__button-back">
					<Link to="/">
						<button className="button-back">
							<img src={img_back_button} alt="Oops" />
						</button>
					</Link>
				</div>
				<div className="content__title">Регистрация</div>
				<form onSubmit={handleSubmit(onSubmit)} className="content__form-item">
					<div className="form-item">
						<input
							{...register("Email", {
								required: true,
								pattern: {
									value: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
								},
							})}
							onChange={(Event) => {
								setEmail(Event.target.value);
								console.log(email);
							}}
							type="email"
							placeholder="Почта"
						/>
						{errors?.Email?.type === "required" && <p>Поля пустое</p>}
						{errors?.Email?.type === "pattern" && <p>Некорректно заполнение</p>}
					</div>
					<div className="form-item">
						<input
							{...register("Password", {
								minLength: 6,
								required: true,
							})}
							onChange={(Event) => {
								setPassword(Event.target.value);
								console.log(password);
							}}
							type="password"
							placeholder="Пароль"
						/>
						{errors?.Password?.type === "required" && <p>Поля пустое</p>}
						{errors?.Password?.type === "minLength" && <p>Мин 6 символов</p>}
					</div>
					<div className="form-item">
						<input type="submit" className="button-next" />
					</div>
				</form>
				<div className="content__text">
					<div className="text">
						Уже зарегистрировались?{" "}
						<Link to="/login" className="text">
							Войти
						</Link>
					</div>
				</div>
			</div>
		</body>
	);
}
export default Register;
