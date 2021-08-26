import * as actions from "../../store/actions/actions";
import { ThunkDispatch } from "redux-thunk";
import { httpClient } from "../httpClient";
import MenuBuilder from "./menuBuilder";

export const initialiseMenu = () => {
    const builder = new MenuBuilder();
    const url = builder.GetMenuEndpoint();

    console.log("initialiseMenu: ", url);
    return (dispatch: ThunkDispatch<{}, void, any>) => {
        httpClient.get(url)
              .then(response => {
                   console.log("initialiseMenu: ",response.statusText)
                   if (response.statusText !== "OK") {
                        throw Error(response.statusText);
                   }
                   dispatch(actions.initMenu(response.data));
              })
              .catch(error => {
                   console.log(error);
              });
     }
};