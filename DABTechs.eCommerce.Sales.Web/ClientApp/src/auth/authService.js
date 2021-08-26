import { IDENTITY_CONFIG, METADATA_OIDC } from "../config/authConfig";
import { UserManager, WebStorageStateStore, Log } from "oidc-client";

export default class AuthService {
    userManager;
    constructor() {
        console.log("AuthService started...", process.env.REACT_APP_AUTH_URL);
        console.log("AuthService config...", IDENTITY_CONFIG);

        this.userManager = new UserManager({
            ...IDENTITY_CONFIG,
            userStore: new WebStorageStateStore({ store: window.sessionStorage }),
            metadata: {
                ...METADATA_OIDC
            }
        });

        // Logger
        Log.logger = console;
        Log.level = Log.DEBUG;
        this.userManager.events.addUserLoaded((user) => {
            console.log("addUserLoaded");
            if (window.location.href.indexOf("signin-oidc") !== -1) {
                //this.navigateToScreen();
            }
        });

        this.userManager.events.addSilentRenewError((e) => {
            console.log("silent renew error", e.message);
        });

        this.userManager.events.addAccessTokenExpired(() => {
            console.log("token expired");
            this.signinSilent();
        });
    }

    signinRedirectCallback = () => {
        console.log("signinRedirectCallback from identity");
        console.log(this.userManager);
        this.userManager.signinRedirectCallback().then(user => {
            console.log(user);
            console.log("signinRedirectCallback 2");
            if (window.location.href.indexOf("signin-oidc") !== -1) {
                this.navigateToScreen();
            }
        }).catch(
            err => {
                console.log(err);
            }
        );
    };


    getUser = async () => {
        console.log("getting user");
        const user = await this.userManager.getUser();
        if (!user) {
            console.log("getting user 2");
            return await this.userManager.signinRedirectCallback();
        }
        console.log("return getting user");
        return user;
    };

    parseJwt = (token) => {
        console.log("parse jwt");
        const base64Url = token.split(".")[1];
        const base64 = base64Url.replace("-", "+").replace("_", "/");
        return JSON.parse(window.atob(base64));
    };


    signinRedirect = () => {
        console.log("sign in redirect");
        sessionStorage.setItem("redirectUri", window.location.pathname);
        this.userManager.signinRedirect({});
    };


    navigateToScreen = () => {
        console.log("navigateToScreen");
        window.location.replace("/");
    };


    isAuthenticated = () => {
        console.log("isAuthenticated");
        console.log(sessionStorage.getItem(`oidc.user:${process.env.REACT_APP_AUTH_URL}:${process.env.REACT_APP_IDENTITY_CLIENT_ID}`));
        const oidcStorage = JSON.parse(sessionStorage.getItem(`oidc.user:${process.env.REACT_APP_AUTH_URL}:${process.env.REACT_APP_IDENTITY_CLIENT_ID}`))

        console.log("isAuthenticated storage = ", oidcStorage);

        console.log("isAuthenticated = ", (!!oidcStorage && !!oidcStorage.id_token));

        const isAuth = (!!oidcStorage && !!oidcStorage.id_token);

        console.log("isAuthenticated = ", isAuth);

        return isAuth;
    };

    signinSilent = () => {
        console.log("signinSilent");
        this.userManager.signinSilent()
            .then((user) => {
                console.log("signed in", user);
            })
            .catch((err) => {
                console.log(err);
            });
    };
    signinSilentCallback = () => {
        this.userManager.signinSilentCallback();
    };

    createSigninRequest = () => {
        console.log("createSigninRequest");
        return this.userManager.createSigninRequest();
    };

    logout = () => {
        this.userManager.signoutRedirect({
            id_token_hint: localStorage.getItem("id_token")
        });
        this.userManager.clearStaleState();
    };

    signoutRedirectCallback = () => {
        this.userManager.signoutRedirectCallback().then(() => {
            localStorage.clear();
            window.location.replace(process.env.REACT_APP_PUBLIC_URL);
        });
        this.userManager.clearStaleState();
    };
}