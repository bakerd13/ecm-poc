import React from "react";
import SearchBox from "./SearchBox";


class SearchBoxMobileContainer extends React.Component {

    render() {


        if(this.props.searchOpen)
        {
            return (
                <div className="searchBoxContainer">
                    <SearchBox />
                </div>);
        }

        return null;
    }
}

export default SearchBoxMobileContainer;