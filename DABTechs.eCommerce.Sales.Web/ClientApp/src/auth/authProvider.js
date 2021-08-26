import React, {Component} from "react";
import AuthService from "./authService";
import AuthContext from "./authContext";

export class AuthProvider extends Component {
    authService;
    constructor(props) {
        super(props);
        console.log("AuthProvider");
        this.authService = new AuthService();
    }
    render() {
        return <AuthContext.Provider value={this.authService}>{this.props.children}</AuthContext.Provider>;
    }
}