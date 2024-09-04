import React, {useEffect, useState} from 'react';
import {Navigate} from "react-router-dom";

function AdminRoute({children}) {
    
    const [isAdmin, setIsAdmin] = useState(null);

    useEffect( () => {
        async function fetchIsAdmin(){
            const token = localStorage.getItem('token');
            const url = 'api/isadmin';

            const response = await fetch(url, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`
                }});
          
            if(response.ok){
                const data = await response.json();
                setIsAdmin(() => data);
            } else {
                setIsAdmin(false);
            }
        }
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