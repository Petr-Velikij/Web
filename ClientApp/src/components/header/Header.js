import React, {Component} from "react";
import {Link } from "react-router-dom";

import "../styles/css/main.css";

function Header(){

  return(
    <header className="header">
          <div className="header__content">
            <div className="header__items"> 
              <div>Lorem</div>
              <div>Lorem</div>
              <div>Lorem</div>
              <div>
              <Link to="login"><button className="button">Войти</button></Link>
              </div>
            </div>
          </div>
    </header>
  )

}

export default Header;