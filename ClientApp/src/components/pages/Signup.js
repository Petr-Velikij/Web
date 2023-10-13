import React, {useEffect} from "react";
import {Link} from "react-router-dom";
import Header from "../header/Header";

import img from "../styles/img/back-button.svg"

import "../styles/css/signup.css"

function Signup(){
  
  
    const color = "#E8E7E7"
   useEffect(() => {document.body.style.backgroundColor = color;})

    return(
    <body>
      <div className="signup">
        <div className="signup__block">
          <button className="button_null"><img  src={img}/></button>
           <div className="signup__items">

            <div  className="text">Вход</div>

            <input className="input" type="text" placeholder="Логин" />
            <input className="input" type="password" placeholder="Пароль" />
            <button className="button" >Продолжить</button>
            <div>

          </div>
          </div>
        </div>
      </div>
    </body>
    )
  
}
export default Signup;