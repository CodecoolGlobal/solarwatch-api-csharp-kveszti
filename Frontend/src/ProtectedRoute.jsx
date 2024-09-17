import React, {useEffect} from "react";
import {Navigate, useNavigate} from "react-router-dom";
import {useAuth} from "./AuthProvider.jsx";

export default function ProtectedRoute({children}){
    
    const { auth, logout, isAdmin, fetchIsAdmin } = useAuth();
    const navigate  = useNavigate();

    async function isExpired(){
        try {
            const url = "api/isExpired";
            const token = localStorage.getItem('token');
            const response = await fetch(url, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`
                }});
            if(response.status === 401){
              navigate("/login");
            }
            return await response.json();
        } catch (err){
            console.log(err, "Something went wrong while checking token expiration.")
        }
    }
    
    useEffect( () => {
        isExpired();
        fetchIsAdmin();
    }, []);

    if (!auth.isAuthenticated) {
        return <Navigate to="/login" />;
    }

    return (
        <div className="protectedContents">
            <div className='buttonContainer'>
                <button className="logoutButton" onClick={() => logout()}>Logout</button>
                {isAdmin? <button className="adminButton" onClick={() => navigate('/admin')}>Admin page</button> : ''}
            </div>
            {children}
        </div>
)
    ;

}