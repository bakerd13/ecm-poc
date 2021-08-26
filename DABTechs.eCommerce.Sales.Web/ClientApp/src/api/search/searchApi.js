import * as actions from "../../store/actions/actions";
import { useDispatch } from "react-redux";
import SearchBuilder from "./searchBuilder";
import { httpClient } from "../httpClient";

// TODO make sure that the api send OK ore bad response.

export const initialiseSearch = (query) => {
     const builder = new SearchBuilder(1);
 
     if (builder.IsCategorySearch(query))
     {
          return getCategorySearch(builder, query);
     }
     else {
          return getKeywordSearchPage(builder, query)
     }
 };
 
 export const getSearchPage = (page) => {
     const builder = new SearchBuilder(page);
 
     // TODO make sure that the api send OK ore bad response.
     return dispatch => {
         httpClient.post(builder.GetSearchEndpoint(), builder.GetFilterQuery())
               .then(response => {
                    if (response.statusText !== "OK") {
                         throw Error(response.statusText);
                    }
                    dispatch(actions.getSearchPage(response.data));
               })
               .catch(error => {
                    console.log(error);
               });
      }
 };
 
 export const UpdateFacet = (facetname, facetOption, checked) => {
      console.log("UpdateFacet called and = ", checked);
     const builder = new SearchBuilder(1);
     const url = builder.GetSearchEndpoint();
     builder.UpdateFacet(facetname, facetOption, checked);

     // TODO make sure that the api send OK ore bad response.
     return dispatch => {
          httpClient.post(url, builder.GetFilterQuery())
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

const getCategorySearch = (builder, query) => {
     const querybuilder = new SearchBuilder(null);
     
     builder.UpdateCategory(querybuilder.ExtractCategoryFilters(query));

      return dispatch => {
           httpClient.post(builder.GetSearchEndpoint(), builder.GetFilterQuery())
                .then(response => {
                     if (response.statusText !== "OK") {
                          throw Error(response.statusText);
                     }
                     dispatch(actions.init(response.data));
                })
                .catch(error => {
                     console.log(error);
                });
      }
 };
 
const getKeywordSearchPage = (builder, keyword) => {
     builder.UpdateKeyword(keyword);
 
     return dispatch => {
          httpClient.post(builder.GetSearchEndpoint(), builder.GetFilterQuery())
               .then(response => {
                    if (response.statusText !== "OK") {
                         throw Error(response.statusText);
                    }
                    dispatch(actions.init(response.data));
               })
               .catch(error => {
                    console.log(error);
               });
     }
 };
 

 
 export const applyPriceChanges = (from, to) => {
      //const builder = new SearchBuilder(1, null);
      // const s = builder.updateSearchQuery(link, true);
  
      console.log("applyPriceChanges from = ", from);
      console.log("applyPriceChanges to = ", to);
 
 
    
         return null;
      //    dispatch => {
      //         dispatch(actions.changePriceRange(from, to, response.data));
      //    }
  }