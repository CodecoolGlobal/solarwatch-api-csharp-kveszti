import {useState} from "react";
import {Link} from "react-router-dom";
import styled from "styled-components";


export default function LoginForm({handleLogin, LinkButton, TransferTextDiv, StyledForm, FormLabel, TextInput, SubmitButton, FormContainerDiv}){
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    
 
    
    return(
        <><FormContainerDiv>
            <StyledForm onSubmit={(e) => handleLogin(e, email, password)}>
                <FormLabel htmlFor="email">
                    E-mail:
                    <TextInput type="email" name="email" required onChange={(e) => setEmail(e.target.value)}></TextInput>
                </FormLabel>
                <FormLabel htmlFor="pswd">
                    Password:
                    <TextInput type="password" name="password" required
                           onChange={(e) => setPassword(e.target.value)}></TextInput> </FormLabel>
                <SubmitButton key="logIn" type="submit">Log in</SubmitButton>
            </StyledForm></FormContainerDiv>
            <div><TransferTextDiv>Need an account?</TransferTextDiv>
                <Link to="/register">
                    <LinkButton>Register</LinkButton>
                </Link></div>
        </>
    )
}