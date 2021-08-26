import React from "react";
import { Route } from "react-router-dom";
import { AuthConsumer } from "../auth/authConsumer";

// TODO loading spinner <span>loading</span>
export const ProtectedRoute = ({ component, ...rest }) => {
    const renderFn = (Component) => (props) => {
console.log("protected route");
        return (
            <AuthConsumer>
                {({ isAuthenticated, signinRedirect }) => {
                    if (!!Component && isAuthenticated()) {
                        return <Component {...props} />;
                    } else {
                        signinRedirect();
                        return <span>loading</span>;
                    }
                }}
            </AuthConsumer>
        );
    }
    

    return <Route {...rest} render={renderFn(component)} />;
};