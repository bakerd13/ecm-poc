import { store } from "../index";
import * as actionTypes from "../store/actions/actionTypes";
import * as constants from "../components/common/constants";

// TODO re-use config
//import { config } from "Config";

// TODO look at how we do preice range
// look at using default price filter in store

export default class BagBuilder {
    constructor(item) {
       
            //const state = store.getState();

            this.ShoppingBagItem = {
                bag: item
            }

    }

    GetBagApiUrl = () => {
        // TODO get api endpoint from Config
        return "http://localhost:6000";
    }

    GetShoppingBagEndpoint = () => {
        // TODO get api endpoint from Config
        return this.GetBagApiUrl() + "/vipsearch/search";
    }

    GetBag = () => {
        // TODO get api endpoint from Config
        return this.ShoppingBagItem;
    }

    UpdateShoppingBag = (item) => {
        // const updatefilterQuery = {
        //     keywordSearch: value,
        //     categorySearch: [],
        //     searchFilters: [],
        //     selectedFilters: [],
        //     searchPriceFilter: {
        //         priceMin: 1,
        //         priceMax: 999999,
        //         priceFrom: 1,
        //         priceTo: 999999
        //     },
        //     sortBy: "bst"
        // }

        // store.dispatch({
        //     type: actionTypes.SETNEWSEARCH,
        //     payload: {
        //         data: updatefilterQuery
        //     }});

        //     this.filterQuery = updatefilterQuery;
        //     this.filterQuery.pageNumber = this.page;
        console.log("TODO");
    }
    
}
