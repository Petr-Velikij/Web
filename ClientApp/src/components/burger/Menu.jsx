import { useState } from "react";

function Menu(active, setActive) {
	//Для будущего получение данных для полей ниже
	const [user, setUser] = useState();
	return (
		<body>
			<div className={active ? "burger active" : "burger"}>
				<div className="burger__content">
					<div className="content__user">
						<button className="button-user">
							<main>
								<p></p>
								<p></p>
							</main>
						</button>
					</div>
					<div className="content__params">
						<button className="button-params"></button>
						<button className="button-params"></button>
						<button className="button-params"></button>
						<button className="button-params"></button>
					</div>
				</div>
			</div>
		</body>
	);
}

export default Menu;
