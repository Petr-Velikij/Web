import React, { useState } from "react";
import { useForm } from "react-hook-form";

import axios from "axios";

function FormTask() {
	const [textTask, setTextTask] = useState("");
	const {
		register,
		handleSubmit,
		formState: { errors },
	} = useForm();

	const urlServer = "https://25.81.29.249:7045/api/lesson";

	const addTask = async () => {
		await axios
			.post(
				urlServer,
				{
					Task: textTask,
				},
				{ headers: { Authorization: "Bearer " + sessionStorage.getItem("Token") } }
			)
			.then((DataText) => {
				console.log(DataText.data);
			})
			.catch(() => {
				console.log("Ошибка отправки");
			});
	};

	return (
		<div className="add-task__container">
			<form onSubmit={handleSubmit(addTask)} className="container__content">
				<div className="content__select-item"> {/* Вернуть select (взять из GIT) */}</div>
				<div className="content__main-form">
					{sessionStorage.getItem("Token")}
					<div className="main-form__title">Новый урок</div>
					<div className="main-form">
						<div className="main-form__input-post">
							<textarea
								className="input-post"
								placeholder="Описание занятие"
								value={textTask}
								{...register("TextTask", {
									required: true,
								})}
								onChange={(Event) => {
									setTextTask(Event.target.value);
									console.log(textTask);
								}}></textarea>
							{errors?.TextTask?.type === "required"}
							<button type="submit" className="button-post">
								отправить
							</button>
						</div>
					</div>
				</div>
			</form>
		</div>
	);
}

export default FormTask;
