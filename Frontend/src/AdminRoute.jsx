import React, {useEffect, useState} from 'react';
import {Navigate, useNavigate} from "react-router-dom";
import {useAuth} from "./AuthProvider.jsx";

function AdminRoute({children}) {
    
  const {fetchIsAdmin, isAdmin, logout} = useAuth();
    const navigate  = useNavigate();

    useEffect( () => {
        fetchIsAdmin();
    }, []);
    
    if(isAdmin === null){
        return (
            <>Checking authentication status, please wait...</>
        )
    }
    if(!isAdmin){
        return <Navigate to="/" />;
    }
    
    return (<div className="protectedContents">
            <div className='buttonContainer'>
                <button className="logoutButton" onClick={() => logout()}>Logout</button>
                <button className="adminButton" onClick={() => navigate('/')}>Homepage</button>
            </div>
            {children}</div>
    )
}

export default AdminRoute;