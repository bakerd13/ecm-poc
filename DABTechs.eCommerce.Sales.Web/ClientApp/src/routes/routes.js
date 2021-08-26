import * as React from "react";
import { Route, Switch } from "react-router-dom";
import { Callback } from "../auth/callback";
import { Logout } from "../auth/logout";
import { LogoutCallback } from "../auth/logoutCallback";
import { ProtectedRoute } from "./protectedRoute";
import { SilentRenew } from "../auth/silentRenew";
import Vip from "../Vip"

// TODO import { Register } from "../auth/register";
// <Route exact={true} path="/register" component={Register} />
// TODO import {PublicPage} from "../components/publicPage"
// TODO Public redirect <Route path="/" component={PublicPage} />
// 
export const Routes = (
    <Switch>
        <Route exact={true} path="/signin-oidc" component={Callback} />
        <Route exact={true} path="/logout" component={Logout} />
        <Route exact={true} path="/logout/callback" component={LogoutCallback} />      
        <Route exact={true} path="/silentrenew" component={SilentRenew} />
        <ProtectedRoute path="/" component={Vip} />      
    </Switch>
);

// export const Routes = (
//     <Switch>
//         <Route exact={true} path="/signin-oidc" component={Callback} />
//         <Route exact={true} path="/logout" component={Logout} />
//         <Route exact={true} path="/logout/callback" component={LogoutCallback} />      
//         <Route exact={true} path="/silentrenew" component={SilentRenew} />
//         <Route path="/" component={Vip} />      
//     </Switch>
// );