import React from "react";
import { withRouter } from "react-router-dom";
import { SearchUrl, keywordIdentifier } from "../../config/constants";

import "./SearchBox.css";

// TODO Stle component
class SearchBox extends React.Component {

    state = {
        keywordSearch: ""
    }

    keyPress = (e) => {
        if(e.key === "Enter")
        {
            this.props.history.push(SearchUrl + keywordIdentifier + this.state.keywordSearch);
            this.setState({keywordSearch: ""});
        }
    }

    onChangeHandler = (e) => {
        this.setState({ keywordSearch: e.target.value });
    }

    searchClickHandler = (e) => {
        e.preventDefault();
        this.props.history.push(SearchUrl + keywordIdentifier + this.state.keywordSearch);
        this.setState({keywordSearch: ""});
    }

    render() {
        return (
            <div className="search">
                <div className="search-input-container">
                    <input type="text"
                        className="autosuggest-search"
                        placeholder="Search VIP"
                        name="autosuggest"
                        value={this.state.keywordSearch}
                        onKeyPress={this.keyPress.bind(this)}
                        onChange={this.onChangeHandler.bind(this)}/>
                    <button
                        className="search-button search-button-icon" onClick={this.searchClickHandler} />
                </div>
            </div>);
    }
}


export default withRouter(SearchBox);