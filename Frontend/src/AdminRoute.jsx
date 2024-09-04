import React, {useEffect, useState} from 'react';
import {Navigate} from "react-router-dom";
import {useAuth} from "./AuthProvider.jsx";

function AdminRoute({children}) {
    
  const {fetchIsAdmin, isAdmin} = useAuth();

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
    
    return children
}

export default AdminRoute;