import React, { useEffect } from 'react'
import ReactDOM from 'react-dom'

import Main from './Main';
import Signup from './components/pages/Signup';

import './components/styles/css/reset.css'
import './components/styles/css/main.css'
import './components/styles/css/signup.css'


import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";



const router = createBrowserRouter([
  {
    path: "/",
    element: <Main></Main>,
  },
    {
    path: "signup",
    element: <Signup></Signup>,
  },
  
]);


const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <React.StrictMode>
  
     
    <RouterProvider router={router} />

  </React.StrictMode>

);
