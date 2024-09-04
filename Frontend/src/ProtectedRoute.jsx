import React, {useEffect} from "react";
import {Navigate, useNavigate} from "react-router-dom";
import { useAuth } from "./AuthProvider.jsx";

export default function ProtectedRoute({children}){
    
    const { auth, logout, isAdmin, fetchIsAdmin } = useAuth();
    const navigate  = useNavigate();

    useEffect( () => {
        fetchIsAdmin();
    }, []);

    if (!auth.isAuthenticated) {
        return <Navigate to="/login" />;
    }

    return (<div className="protectedContents"><div className='buttonContainer'><button className="logoutButton" onClick={() => logout()}>Logout</button>{isAdmin? <button className="adminButton" onClick={() => navigate('/admin')}>Admin page</button> : ''}</div>
            {children}</div>
)
    ;

}