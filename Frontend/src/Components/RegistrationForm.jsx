import {useState} from "react";
import {Link} from "react-router-dom";

export default function RegistrationForm({handleSubmit,LinkButton, TransferTextDiv, StyledForm, FormLabel, TextInput, SubmitButton, FormContainerDiv}){
    const [userName, setName] = useState();
    const [email, setEmail] = useState();
    const [password, setPassword] = useState();
    
    return(
        <>
            <FormContainerDiv>
                <StyledForm onSubmit={(e) => handleSubmit(e, userName, email, password)}>
                    <FormLabel htmlFor="userName">
                        Username:
                        <TextInput type="text" name="userName" required onChange={(e) => setName(e.target.value)}></TextInput>
                    </FormLabel>
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
            <div><TransferTextDiv>Already have an account?</TransferTextDiv>
                <Link to="/login">
                    <LinkButton>Log in</LinkButton>
                </Link></div>
        </>
    )
}