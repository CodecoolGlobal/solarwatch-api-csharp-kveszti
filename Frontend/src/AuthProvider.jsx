import React, { createContext, useContext, useState, useEffect } from "react";

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [auth, setAuth] = useState(() => {
        const token = localStorage.getItem("token");
        return token ? { token, isAuthenticated: true } : { token: null, isAuthenticated: false }
    });
    const [isAdmin, setIsAdmin] = useState(null);

    const login = (token) => {
        localStorage.setItem("token", token);
        setAuth({ token, isAuthenticated: true });
    };

    const logout = () => {
        localStorage.removeItem("token");
        setAuth({ token: null, isAuthenticated: false });
    };

    const fetchIsAdmin = async() => {
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
    return (
        <AuthContext.Provider value={{ auth, login, logout, isAdmin, fetchIsAdmin }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => useContext(AuthContext);
