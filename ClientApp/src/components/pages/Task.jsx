import React, { useState } from "react";
import { HeaderAccount } from "../header/Headers";
import FormTask from "../list/FormTask";

function Task() {
	const [task, setTask] = useState([]);
	return (
		<div>
			<HeaderAccount></HeaderAccount>
			<div className="all-content">
				<div className="lesson__content">
					<FormTask></FormTask>
					<ul></ul>
				</div>
			</div>
		</div>
	);
}

export default Task;
