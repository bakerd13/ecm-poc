import React from "react";
import { AuthConsumer } from "./authConsumer";

export const Callback = () => (
    <AuthConsumer>
        {({ signinRedirectCallback }) => {
            signinRedirectCallback();
            return <span>loading</span>;
        }}
    </AuthConsumer>
);