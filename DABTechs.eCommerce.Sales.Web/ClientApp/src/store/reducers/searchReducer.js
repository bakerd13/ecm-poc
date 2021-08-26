import * as actionTypes from "../actions/actionTypes";
import defaultSearchQuery from "../defaultSearchQuery.json";

// TODO Get rid off SearchResultsHeader not required for VIP
// also need to do lastUpdatedTime
const initialState = {
    isLoading: true,
    searchResults:[],
    totalMatchingResults: 0,
    lastUpdatedDateTimeStamp: null,
    searchQuery:  defaultSearchQuery
}

// TODO what is searchQuery doing
const searchReducer = (state = initialState, action) => {
    switch (action.type) {
        case actionTypes.INITIALISE:
            console.log("reducer", action.payload.data.productItems);
            console.log("reducer", action.payload.data.totalMatchingResults);
            return {
                ...state,
                isLoading: false,
                searchResults: [{results: action.payload.data.productItems }],
                totalMatchingResults: action.payload.data.totalMatchingResults,
                lastUpdatedDateTimeStamp: action.payload.data.lastUpdatedDateTimeStamp,
                searchQuery: action.payload.data.searchQuery
            };

        case actionTypes.GETNEWSEARCH:
            return {
                ...state,
                searchResults: [{ results: action.payload.data.productItems }],
                totalMatchingResults: action.payload.data.totalMatchingResults,
            };

            case actionTypes.GETUPDATEDSEARCH:
                return {
                    ...state,
                    searchResults: [{ results: action.payload.data.productItems }],
                    totalMatchingResults: action.payload.data.totalMatchingResults,
                };

        case actionTypes.GETSEARCHPAGE:
            return {
                ...state,
                searchResults: state.searchResults.concat({ results: action.payload.data.productItems }),
                totalMatchingResults: action.payload.data.totalMatchingResults,
            };


        case actionTypes.GETFILTEREDSEARCHPAGE:
            console.log(actionTypes.GETFILTEREDSEARCHPAGE, action.payload);
            return {
                ...state,
                searchResults: state.searchResults.concat({ page: action.payload.data.page, results: action.payload.data.productItems }),
            };

        default:
            return state;
    }
}

export default searchReducer;