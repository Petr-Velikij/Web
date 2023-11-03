import React, { useState, Fragment } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";
import axios from "axios";

function Register() {
	const [email, setEmail] = useState("");
	const [password, setPassword] = useState("");
	const navigate = useNavigate();
	const {
		register,
		handleSubmit,
		formState: { errors },
	} = useForm();

	const onSubmit = async () => {
		const urlServer = "https://25.81.29.249:7045/api/persons";

		await axios
			.post(urlServer, {
				Email: email,
				Password: password,
			})
			.then((DataSQL) => {
				return console.log(DataSQL.data), navigate("/login");
			})
			.catch(() => {
				console.log("ошибка");
			});
	};
	return (
		<Fragment>
			<div className="all-content">
				<div className="authentication">
					<div className="authentication-container__content">
						<div className="content__button-back">
							<Link to="/">
								<button className="button-back"></button>
							</Link>
						</div>
						<div className="content__title">Регистрация</div>
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
									placeholder="Почта"
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
							<div className="form-item">
								<button type="submit" className="button-next">
									Продолжить
								</button>
							</div>
						</form>
						<div className="content__text">
							<div>
								Уже зарегистрировались?{" "}
								<Link to="/login" className="link">
									Войти
								</Link>
							</div>
						</div>
					</div>
				</div>
			</div>
		</Fragment>
	);
}
export default Register;
