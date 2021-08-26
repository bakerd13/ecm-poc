import * as actionTypes from "./actionTypes";

// TODO revist all of these some are now not used

export const init = (data) => {
    return {
        type: actionTypes.INITIALISE,
        payload: {
            page: 1,
            data: data
        }
    };
}

export const initMenu = (data) => {
    return {
        type: actionTypes.INITIALISEMENU,
        payload: {
            data: data
        }
    };
}



export const getSearchPage = (data) => {
    return {
        type: actionTypes.GETSEARCHPAGE,
        payload: {
            data: data
        }
    };
}

export const getNewSearch = (data) => {
    console.log("getNewSearch===", data);
    return {
        type: actionTypes.GETNEWSEARCH,
        payload: {
            data: data
        }
    };
}

export const getUpdatedSearch = (data) => {
    return {
        type: actionTypes.GETUPDATEDSEARCH,
        payload: {
            data: data
        }
    };
}

export const getFilteredSearchPage = (page, data) => {
    return {
        type: actionTypes.GETFILTEREDSEARCHPAGE,
        payload: {
            page: page,
            data: data
        }
    };
}

export const setSelectedFilters = (facetName, optionValue, checked) => {
        return {
            type: actionTypes.SETSELECTEDFILTER,
            payload: {
                facetName: facetName, 
                optionValue: optionValue,
                checked: checked
            }
        };
}

export const clearSelectedFilterGroup = (filterName) => {
    return {
        type: actionTypes.CLEARGROUPFILTER,
        payload: {
            filterName: filterName
        }
    };
}


export const changePriceRange = (from, to, data) => {
    return {
        type: actionTypes.CHANGEPRICERANGE,
        payload: {
            priceFrom: from,
            priceTo: to,
            data: data
        }
    };
}

