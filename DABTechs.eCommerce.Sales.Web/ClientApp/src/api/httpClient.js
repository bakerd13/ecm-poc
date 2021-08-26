import axios from "axios";

export const httpClient = axios.create();

httpClient.interceptors.request.use((config) => {

  const oidcStorage = JSON.parse(sessionStorage.getItem(`oidc.user:${process.env.REACT_APP_AUTH_URL}:${process.env.REACT_APP_IDENTITY_CLIENT_ID}`));

  console.log("oidcStorage.access_token: ", oidcStorage.access_token);

  config.withCredentials = false;
  config.headers.Accept = "application/json";
  config.headers.ContentType = "application/json";
  
  config.headers.Authorization = `Bearer ${oidcStorage.access_token}`;

  console.log("auth header: ", config.headers.Authorization);
  return config;
}, (error) => {
  return Promise.reject(error);
}
);

