import * as actionTypes from "../actions/actionTypes";
import * as helpers from "./reducerHelpers"; 


const initialState = {
    keywordSearch: "",
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

const filterReducer = (state = initialState, action) => {
    switch (action.type) {
        case actionTypes.INITIALISE:
            return {
                ...state,
                searchFilters: state.searchFilters.concat(action.payload.data.productFilters.filters),
                selectedFilters: [],
            };

            case actionTypes.GETNEWSEARCH:
                return {
                    ...state,
                    searchFilters: [...action.payload.data.productFilters.filters],
                    selectedFilters: [],
                };

        case actionTypes.SETNEWSEARCH:
            return {
                ...state,
                ...action.payload.data,
            };

        case actionTypes.SETSELECTEDFILTER:
            if (action.payload.checked) {
                return {
                    ...state,
                    selectedFilters: state.selectedFilters.concat(action.payload)
                };
            }
            else {
                // TODO check optionValue should it facetName
                const newselectedFilters = state.selectedFilters.filter((i) => { return !(i.Name === action.payload.Name && i.filterItems.facetName === action.payload.facetName) });
                return {
                    ...state,
                    selectedFilters: newselectedFilters
                };
            }

        case actionTypes.CLEARSELECTEDALLFILTER:
            return {
                ...state,
                selectedFilters: [],
            };
        case actionTypes.CLEARGROUPFILTER:
            const newselectedFilters = state.selectedFilters.filter((i) => { return !(i.Name === action.payload.Name) });
            return {
                ...state,
                selectedFilters: newselectedFilters
            };

        case actionTypes.KEYWORDSEARCH:
            return {
                ...state,
                selectedFilters: [],
            };
        case actionTypes.CHANGEPRICERANGE:
            return {
                ...state,
                searchPriceFilter: {
                    priceFrom: action.payload.priceFrom,
                    priceTo: action.payload.priceTo
                },
            };
        default:
            return state;
    }
}

export default filterReducer;