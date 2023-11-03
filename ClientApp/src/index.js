import React from "react";
import ReactDOM from "react-dom/client";

import Main from "./Main";
import Login from "./components/pages/Login";
import Register from "./components/pages/Register";
import Account from "./components/pages/Account";
import Task from "./components/pages/Task";

import "./components/styles/css/global.css";

import { createBrowserRouter, RouterProvider } from "react-router-dom";

const router = createBrowserRouter([
	{
		path: "/",
		element: <Main></Main>,
	},
	{
		path: "login",
		element: <Login></Login>,
	},
	{
		path: "register",
		element: <Register></Register>,
	},
	{
		path: "account",
		element: <Account></Account>,
	},
	{
		path: "task",
		element: <Task></Task>,
	},
]);

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
	<React.StrictMode>
		<RouterProvider router={router} />
	</React.StrictMode>
);
