import { HeaderMain } from "./components/header/Headers";
import "./components/styles/css/main.css";

import React, { useEffect } from "react";

function Main() {
	const color = "#E8E7E7";

	useEffect(() => {
		document.body.style.backgroundColor = color;
	});
	return (
		<div className="Main">
			<HeaderMain></HeaderMain>
		</div>
	);
}

export default Main;
