import React, { useEffect } from "react";
import { HeaderAccount } from "../header/Headers";

function Account() {
	const color = "#E8E7E7";
	useEffect(() => {
		document.body.style.backgroundColor = color;
	});
	return (
		<HeaderAccount>
			<div>Hello</div>
		</HeaderAccount>
	);
}

export default Account;
