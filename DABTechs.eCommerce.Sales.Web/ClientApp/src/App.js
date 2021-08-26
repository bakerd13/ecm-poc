import React from 'react';
import { BrowserRouter } from "react-router-dom";
import { Routes } from "./routes/routes";
import { AuthProvider } from "./auth/authProvider";


import "./App.css";

class App extends React.Component {
    render() {
        return (
            <AuthProvider>
                <BrowserRouter children={Routes} basename="/" >                   
                </BrowserRouter>
            </AuthProvider>
        );
    }
}

export default App;
