import * as actions from "./actions";
import axios from "axios";
import SearchBuilder from "../../api/search/searchBuilder";
import BagBuilder from "../../helpers/bagBuilder";

const transport = axios.create({
    headers: {
     "Accept": "application/json",
     "Content-Type": "application/json"
    },
    withCredentials: false
  });





 export const AddToBag = (item) => {
     const builder = new BagBuilder(item);
     const url = builder.GetShoppingBagEndpoint();
     builder.UpdateShoppingBag(item);

     // TODO make sure that the api send OK ore bad response.
     return dispatch => {
          transport.post(url, builder.GetBag())
               .then(response => {
                    if (response.statusText !== "OK") {
                         throw Error(response.statusText);
                    }
                    dispatch(actions.getUpdatedSearch(response.data));
               })
               .catch(error => {
                    console.log(error);
               });
     }
};