import * as actionTypes from "../actions/actionTypes";

// TODO Get rid off SearchResultsHeader not required for VIP
// also need to do lastUpdatedTime
const initialState = {
    shoppingBagId: "",
    items: []
}


const searchReducer = (state = initialState, action) => {
    switch (action.type) {
        case actionTypes.INITIALISE:
            // TODO get items from shoppingBad service
            return {
                ...state,
                items: []
            };
        default:
            return state;
    }
}

export default searchReducer;