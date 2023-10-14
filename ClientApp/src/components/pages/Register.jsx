import React, {useEffect} from "react";
import {Link} from "react-router-dom";
import Header from "../header/Header";
import Login from "./Login";

import img_back_button from "../styles/img/back-button.svg"

import "../styles/css/register.css"

function Register(){
  
  
    const color = "#E8E7E7"
   useEffect(() => {document.body.style.backgroundColor = color;})

    return(
    <body className="register">
      <div className="register-container__content">
        <div className="content__button-back">
          <Link to="/"><button className="button-back"><img src={img_back_button} alt="Oops" /></button></Link>
        </div>
        <div className="content__title">Регистрация</div>
        <div className="content__form-item">
          <div className="form-item">
            <input type="text" placeholder="Фамилия"></input>
          </div>
           <div className="form-item">
            <input type="password" placeholder="Имя"></input>
          </div>
          <div className="form-item">
            <input type="password" placeholder="Почта"></input>
          </div>
          <div className="form-item">
            <input type="password" placeholder="Телефон"></input>
          </div>
          <div className="form-item">
            <input type="password" placeholder="Пароль"></input>
          </div>
          <div className="form-item">
            <input type="password" placeholder="Подтверждения пароля"></input>
          </div>
          <div className="form-item">
            <button className="button-next">Продолжить</button>
          </div>
          
          </div>
        <div className="content__text">
          <div className="text" >Уже зарегистрировались?  <Link to="/login"className="text">Войти</Link></div>
        </div>
      </div>
    </body>
    )
  
}
export default Register;