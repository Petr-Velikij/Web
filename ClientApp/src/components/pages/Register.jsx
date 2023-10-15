import React, { useEffect, useState } from 'react'
import { Link } from 'react-router-dom'
import Axios from 'axios'

import Header from '../header/Header'
import Login from './Login'

import img_back_button from '../styles/img/back-button.svg'

import '../styles/css/register.css'
import axios from 'axios'

function Register() {
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')

  const SaveData = () => {
    const userData = {
      Email: "petr@gmail.com",
    }

      const urlServer = '/Test/Register'
    axios.post(urlServer, null).then(result => {
      console.log(result.data)

    })
      .catch(error => {
        alert(error)
      })
  }

  const color = '#E8E7E7'
  useEffect(() => {
    document.body.style.backgroundColor = color
  })

  return (
      <body className='register'>
          <form method="post" action="~/Home/Area">
              <label>Высота:</label><br />
              <input type="number" name="height" /><br />
              <label>Основание:</label><br />
              <input type="number" name="altitude" /><br />
              <input type="submit" value="Отправить" />
          </form>
      <div className='register-container__content'>
        <div className='content__button-back'>
          <Link to='/'>
            <button className='button-back'>
              <img src={img_back_button} alt='Oops' />
            </button>
          </Link>
        </div>
        <div className='content__title'>Регистрация</div>
        <div className='content__form-item'>
          <div className='form-item'>
            <input
              type='email'
              placeholder='Почта'
          
            ></input>
          </div>
          <div className='form-item'>
            <input
              type='password'
              placeholder='Пароль'
             
            ></input>
          </div>
          <div className='form-item'>
            <button className='button-next' onClick={SaveData}>
              Продолжить
            </button>
          </div>
        </div>
        <div className='content__text'>
          <div className='text'>
            Уже зарегистрировались?{' '}
            <Link to='/login' className='text'>
              Войти
            </Link>
          </div>
        </div>
      </div>
    </body>
  )
}
export default Register
