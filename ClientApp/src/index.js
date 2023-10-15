import React, { useEffect } from 'react'
import ReactDOM from 'react-dom'

import Main from './Main'
import Login from './components/pages/Login'
import Register from './components/pages/Register'

import './components/styles/css/reset.css'
import './components/styles/css/main.css'
import './components/styles/css/login.css'

import { createBrowserRouter, RouterProvider } from 'react-router-dom'

const router = createBrowserRouter([
  {
    path: '/',
    element: <Main></Main>,
  },
  {
    path: 'login',
    element: <Login></Login>,
  },
  {
    path: 'register',
    element: <Register></Register>,
  },
])

const root = ReactDOM.createRoot(document.getElementById('root'))
root.render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
)
