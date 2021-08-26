import * as React from "react";
import { Route, Switch } from "react-router-dom";
import StoreFront from "../components/storeFront/storeFront";
import ProductListingPage from "../components/pages/ProductListingPage";
import ShoppingBag from "../components/shoppingbag/ShoppingBag";
import Wrapper from "../components/common/Wrapper";

// TODO import { Register } from "../auth/register";
// <Route exact={true} path="/register" component={Register} />
// TODO import {PublicPage} from "../components/publicPage"
// TODO Public redirect <Route path="/" component={PublicPage} />
//
export const MainRoutes = (
<Wrapper>
        <Route exact={true} path="/search" component={ProductListingPage} />
        <Route exact={true} path="/shoppingbag" component={ShoppingBag} />
        <Route path="/" component={StoreFront} />
</Wrapper>
);