function FormTask() {
	return (
		<div className="add-task__container">
			<div className="container__content">
				<div className="content__select-item">
					<select className="select-item">Выбор времени/места</select>
					<select className="select-item">Выбор группы</select>
				</div>
				<div className="content__main-form">
					<div className="main-form">
						<ul className="main-form__group-button">
							<li className="li-form">
								<button>sgh</button>
								<button>s1123</button>
								<button>aas</button>
							</li>
							<li className="li-form">
								<button>s</button>
								<button>asd</button>
								<button>asda</button>
							</li>
							<li className="li-form">
								<button>asdas</button>
								<button>asdasd</button>
								<button>asd</button>
							</li>
						</ul>
						<div className="main-form__input-post">
							<textarea className="input-post" placeholder="Напишите что-нибудь"></textarea>

							<button type="submit" className="button-post">
								отправить
							</button>
						</div>
					</div>
				</div>
			</div>
		</div>
	);
}

export default FormTask;
