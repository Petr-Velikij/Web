import React, {useEffect} from "react";
import {Link} from "react-router-dom";
import Header from "../header/Header";

import "../styles/css/signup.css"

function Signup(){
  
  
    const color = "#E8E7E7"
   useEffect(() => {document.body.style.backgroundColor = color;})

    return(
    <body>
      <div className="signup">
        <div className="signup__block">
         <span className="">Вход</span>
          <div className="signup__items">

            <input className="input" type="text" />
            <input className="input" type="text" />
            <button className="input">Продолжить</button>

          </div>
          <div>
            <span >Забыли пароль?</span>
            <p><span>Не учетной записи? <Link>Зарегистрируйтесь</Link></span></p>
          </div>
        </div>
      </div>
    </body>
    )
  
}
export default Signup;