import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";
import axios from "axios";

function Login() {
	const [email, setEmail] = useState();
	const [password, setPassword] = useState();
	const navigate = useNavigate();
	const {
		register,
		handleSubmit,
		formState: { errors },
	} = useForm();

	const onSubmit = async (Event) => {
		const urlServer = "https://25.81.29.249:7045/api/persons";

		await axios
			.put(urlServer, {
				Email: email,
				Password: password,
			})
			.then((DataSQL) => {
				if (DataSQL.data === true) {
					return navigate("/account");
				}
			})
			.catch(() => {
				console.log("Ошибка");
			});
	};
	return (
		<body>
			<div className="all-content">
				<div className="authentication">
					<div className="authentication-container__content">
						<div className="content__button-back">
							<Link to="/">
								<button className="button-back"></button>
							</Link>
						</div>
						<div className="content__title">Вход</div>
						<form onSubmit={handleSubmit(onSubmit)} className="content__form-item">
							<div className="form-item">
								<input
									className="input-authentication"
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
									placeholder="Логин"
								/>
								{errors?.Email?.type === "required" && <p className="p-authentication">Поля пустое</p>}
								{errors?.Email?.type === "pattern" && (
									<p className="p-authentication">Некорректно заполнение</p>
								)}
							</div>
							<div className="form-item">
								<input
									className="input-authentication"
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
								{errors?.Password?.type === "required" && (
									<p className="p-authentication">Поля пустое</p>
								)}
								{errors?.Password?.type === "minLength" && (
									<p className="p-authentication">Мин 6 символов</p>
								)}
							</div>

							<div>
								<button type="submit" className="button-next">
									Продолжить
								</button>
							</div>
						</form>
						<div className="content__text">
							<Link className="link">Забыли пароль?</Link>
							<div>
								Не учетной записи?{" "}
								<Link to="/register" className="link">
									Зарегистрируйтесь
								</Link>
							</div>
						</div>
					</div>
				</div>
			</div>
		</body>
	);
}
export default Login;
