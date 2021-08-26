import { store } from "../../index";
import * as actionTypes from "../../store/actions/actionTypes";
import * as constants from "../../components/common/constants";

// TODO re-use config
//import { config } from "Config";

// TODO look at how we do preice range
// look at using default price filter in store

export default class SearchBuilder {
    constructor(page) {
        this.afValue = "?af=";

        if (page !== null)
        {
            const state = store.getState();

            this.filterQuery = {
             keywordSearch: state.Filter.keywordSearch,
             categorySearch: state.Filter.categorySearch,
             searchFilters: state.Filter.searchFilters,
             selectedFilters: state.Filter.selectedFilters,
             searchPriceFilter: state.Filter.searchPriceFilter,
             sortBy: state.Filter.sortBy,
             pageNumber: page
            };
        }     
    }

    GetSearchApiUrl = () => {
        // TODO get api endpoint from Config
        return process.env.REACT_APP_SEARCHAPI_URL;
    }

    GetSearchEndpoint = () => {
        // TODO get api endpoint from Config
        return this.GetSearchApiUrl() + "/vipsearch/search";
    }



    GetFilterQuery = () => {
        // TODO get api endpoint from Config
        return this.filterQuery;
    }

    UpdateCategory = (value) => {
        const updatefilterQuery = {
            keywordSearch: "",
            categorySearch: value,
            searchFilters: [],
            selectedFilters: [],
            searchPriceFilter: {
                priceMin: 1,
                priceMax: 999999,
                priceFrom: 1,
                priceTo: 999999
            },
            sortBy: "bst"
        }

        store.dispatch({
            type: actionTypes.SETNEWSEARCH,
            payload: {
                data: updatefilterQuery
            }});

            this.filterQuery = updatefilterQuery;
            this.filterQuery.pageNumber = this.page;
    }

    UpdateKeyword = (value) => {
        const updatefilterQuery = {
            keywordSearch: value,
            categorySearch: [],
            searchFilters: [],
            selectedFilters: [],
            searchPriceFilter: {
                priceMin: 1,
                priceMax: 999999,
                priceFrom: 1,
                priceTo: 999999
            },
            sortBy: "bst"
        }

        store.dispatch({
            type: actionTypes.SETNEWSEARCH,
            payload: {
                data: updatefilterQuery
            }});

            this.filterQuery = updatefilterQuery;
            this.filterQuery.pageNumber = this.page;
    }

    UpdateFacet = (facetname, facetOption, checked) => {
        let updatefilterQuery = this.filterQuery;

        console.log("facetname", facetname);
        console.log("facetoption", facetOption);
        console.log("checked", checked);

        if (checked) {
            updatefilterQuery.selectedFilters.push({name: facetname, value: facetOption});
        }
        else {
            // TODO check optionValue should it facetName
            const newselectedFilters = updatefilterQuery.selectedFilters.filter((i) => { return !(i.name === facetname && i.value=== facetOption) });
            console.log("New facets", newselectedFilters);
            updatefilterQuery.selectedFilters = newselectedFilters;
            console.log("updated facets", updatefilterQuery.selectedFilters);
        }

        store.dispatch({
            type: actionTypes.SETNEWSEARCH,
            payload: {
                data: updatefilterQuery
            }});

            this.filterQuery = updatefilterQuery;
            this.filterQuery.pageNumber = this.page;
    }

    IsCategorySearch = (value) => {
        
        if(value !== null || value !== undefined)
        {
            if(value.indexOf(this.afValue) >= 0){
                return true;
            }
        }

        return false;
    }

    ExtractCategoryFilters = (value) => {
        let extractedAfValue = value;

        if(value !== null || value !== undefined)
        {
            const indexOfAfValue = value.indexOf(this.afValue);
            if(indexOfAfValue >= 0){
                extractedAfValue = value.substring(indexOfAfValue + this.afValue.length, value.length);
            }

            return this.GetExtractedFilterKeyValue(unescape(extractedAfValue));
        }

        return [];
    }

    GetExtractedFilterKeyValue = (extractedAfValue) => {
        const items = extractedAfValue.split(" ");
        let keyValues = [];

        for (let i = 0; i <= items.length - 1; i++)
        {
            const item = items[i].split(":");
            const cleanItem = item[1].split("&");

            keyValues.push({name: item[0], value: cleanItem[0]});
        }

        return keyValues;
    }
}
