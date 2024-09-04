import LoginForm from "../Components/LoginForm.jsx";
import {useAuth} from "../AuthProvider.jsx";
import {useNavigate} from "react-router-dom";

export default function Login({
                                  LinkButton,
                                  TransferTextDiv,
                                  StyledForm,
                                  FormLabel,
                                  TextInput,
                                  SubmitButton,
                                  FormContainerDiv
                              }){
    const { login } = useAuth();
    const navigate = useNavigate();
    
    async function HandleLogin(e, email, password){
        e.preventDefault();
        
        const data = {
            email: email,
            password: password,
        };
        
        try {
            const response = await fetch("api/Login", {method: "POST", headers: {'Content-Type': 'application/json'}, body: JSON.stringify(data)});
            
            const incoming = await response.json();
            
            console.log(incoming);
            
            if(incoming.success){
                console.log("Successfully logged in!");
                login(incoming.token);
                navigate("/");
            }
            
        } catch(err){
            console.error("Error while logging in:", err);
        }
    }
    
    return(
        <>
            <h2>Welcome to the login page!</h2>
            <LoginForm handleLogin={HandleLogin} LinkButton={LinkButton} TransferTextDiv={TransferTextDiv} StyledForm={StyledForm} FormLabel={FormLabel} TextInput={TextInput} SubmitButton={SubmitButton} FormContainerDiv={FormContainerDiv}/>
        </>
    )
}