import {useEffect, useState} from "react";
import RegistrationForm from "../Components/RegistrationForm.jsx";
import {Navigate, useNavigate} from "react-router-dom";

export default function Registration({LinkButton, TransferTextDiv, StyledForm, FormLabel, TextInput, SubmitButton, FormContainerDiv}){
    
    const [success, setSuccess] = useState(false);
    const navigate = useNavigate();
    let seconds = 4;
    
    async function HandleSubmit(e, username, email, password){
        e.preventDefault();

        const data = {
            email: email,
            username: username,
            password: password,
        };

        try {
            const response = await fetch("api/Register", {method: "POST", headers: {'Content-Type': 'application/json'}, body: JSON.stringify(data)});

            const incoming = await response.json();

            console.log(incoming);

            if(incoming.success){
                console.log("Successfully registered an account!");
                setSuccess(true);
                
            }

        } catch(err){
            console.error("Error while logging in:", err);
        }
    }

    useEffect(() => {
        if (success === true) {
            setTimeout(() => {
                navigate('/login');
            }, 1000 * seconds);
        }
    }, [success]);
    
    return(
        <>
       <h2>Welcome to the registration page!</h2>
            <RegistrationForm handleSubmit={HandleSubmit} LinkButton={LinkButton} TransferTextDiv={TransferTextDiv} StyledForm={StyledForm} FormLabel={FormLabel} TextInput={TextInput} SubmitButton={SubmitButton} FormContainerDiv={FormContainerDiv}/>
        </>
    )
}