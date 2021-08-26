import * as actionTypes from "../actions/actionTypes";

// TODO Get rid off SearchResultsHeader not required for VIP
// also need to do lastUpdatedTime
const initialState = {
    menu: {}
}


const menuReducer = (state = initialState, action) => {
    switch (action.type) {
        case actionTypes.INITIALISEMENU:
            return {
                ...state,
                menu: (action.payload.data)
            };
        default:
            return state;
    }
}

export default menuReducer;