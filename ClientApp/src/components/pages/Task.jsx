import React, { useState, Fragment, useEffect } from "react";
import { HeaderAccount } from "../header/Headers";

import axios from "axios";

import FormTask from "../list/FormTask";

function Task() {
	var [createTask, setCreateTask] = useState(false);
	var [listTask, setListTask] = useState(true);

	function VisibleTask() {
		setCreateTask(true);
		setListTask(false);
		if (createTask) {
			setCreateTask(false);
			setListTask(true);
		}
	}

	const urlServer = "https://25.81.29.249:7045/api/lesson";

	const [taskItems, setTaskItems] = useState([]);

	const getTaskArray = async () => {
		const Data = await axios.get(urlServer, {
			headers: { Authorization: "Bearer " + sessionStorage.getItem("Token") },
		});
		setTaskItems(Data.data);
	};
	useEffect(() => {
		getTaskArray();
	}, [setTaskItems]); /**Сделать проверку изменение элемента а не каждый "кадр" */

	return (
		<Fragment>
			<HeaderAccount></HeaderAccount>
			<div className="all-content">
				<button className="button-post" onClick={VisibleTask}>
					Создать занятие
				</button>
				<div className="lesson__content">
					{createTask && <FormTask></FormTask>}
					<div>
						{listTask && (
							<ul>
								{taskItems &&
									taskItems.map((item) => {
										return (
											<li key={item.id}>
												<div className="lesson__block-text">
													<main className="block-text">
														<p>{item.task}</p>
													</main>
												</div>
											</li>
										);
									})}
							</ul>
						)}
					</div>
				</div>
			</div>
		</Fragment>
	);
}

export default Task;
