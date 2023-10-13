
import "./components/styles/css/main.css";

import Header from "./components/header/Header";

import React, {useEffect} from "react";



function Main() {

  const color = "#E8E7E7"

   useEffect(() => {
    document.body.style.backgroundColor = color;
  })
  return(
  <div className="Main">
    <Header></Header>
  </div>
  );
}

export default Main;
