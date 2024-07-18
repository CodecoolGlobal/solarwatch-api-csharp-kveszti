import { useState } from 'react'

import './App.css'
import {BrowserRouter, Route, Routes} from "react-router-dom";
import Registration from "./Pages/Registration.jsx";
import Login from "./Pages/Login.jsx";
import SolarWatch from "./Pages/SolarWatch.jsx";
import styled from "styled-components";


const LinkButton = styled.button`background-color: transparent;
  display: inline-block;
  color: #033495;
  text-decoration: underline;
  margin-left: 5px;
  padding: 0px;
    `;

const TransferTextDiv = styled.p`display: inline-block;
  margin-right: 1px;
  padding: 0px;`

const StyledForm = styled.form`display: flex;
  flex-direction: column;
  margin-top: 100px;
  width: 500px;
  border-radius: 25px;`

const FormLabel = styled.label`display: block;
    margin: 5px;
    padding: 3px;
    font-size: 20px;
    color: black;
    text-align: center;
`

const TextInput = styled.input`font-size: large;
    border-radius: 10px;
    margin: auto;
    padding: 5px;
    display: block;
    align-content: center;
`
const SubmitButton = styled.button`
    margin-top: 15px;
`

const FormContainerDiv = styled.div`
background-color: antiquewhite;
    border-radius: 10px;`
function App() {
    

  return (
    <>
      <BrowserRouter>
          <Routes>
              <Route path='/register' element={<Registration LinkButton={LinkButton} TransferTextDiv={TransferTextDiv} StyledForm={StyledForm} FormLabel={FormLabel} TextInput={TextInput} SubmitButton={SubmitButton} FormContainerDiv={FormContainerDiv}/>}></Route>
              <Route path='/login' element={<Login LinkButton={LinkButton} TransferTextDiv={TransferTextDiv} StyledForm={StyledForm} FormLabel={FormLabel} TextInput={TextInput} SubmitButton={SubmitButton} FormContainerDiv={FormContainerDiv}/>}></Route>
              <Route path='/solar-watch' element={<SolarWatch />}></Route>
          </Routes>
      </BrowserRouter>
    </>
  )
}

export default App
