import React from "react";
import { connect } from "react-redux";
import { Filters } from "../searchFilters";
import Results from "../results/Results";
import NoResults from "../common/noResults";
import { initialiseSearch } from "../../api/search";
import { Loading } from "../common/loading";

import "./ProductListingPage.css";

// TODO checkout this import 
//import "../styles/search.css";

// TODO look at searchTern and no results
class ProductListingPage extends React.Component
{
    state = {
        currentSearch: "",
    }

    componentDidMount() {
        console.log("Component did mount");
        this.setState({currentSearch: this.props.location.search});
        this.props.onInitialize(this.props.location.search); 
    }

    componentDidUpdate() {
        if(this.state.currentSearch !== this.props.location.search)
        {
            this.setState({currentSearch: this.props.location.search});
            this.props.onInitialize(this.props.location.search); 
        }
        console.log("Component did update");
    }

    render() {
        let searchComponents = null;
 console.log("TOTAL RESULT...",this.props.totalMatchingResults);
        if (!this.props.isLoading)
        {
            if (this.props.totalMatchingResults > 0)
            {
                searchComponents = (
                    <div className="productListingPage">
                        <div className="filterContainer">
                            <Filters searchFilters={this.props.searchFilters} priceFilters={this.props.priceFilters} totalResults={this.props.totalMatchingResults} productText="Products" />
                        </div>
                        <div className="resultsContainer">
                            <Results searchResults={this.props.searchResults}/>
                        </div>
                    </div>
                );
            }
            else
            {
                searchComponents = (<NoResults SearchTerm={this.state.searchTerm} />);
            }
        }
        else
        {
            searchComponents = (<Loading />);
        }
        
        return searchComponents;
    }
}

const mapStateToProps = state => {
    return {
        isLoading: state.Search.isLoading,
        searchFilters: state.Filter.searchFilters,
        searchResults: state.Search.searchResults,
        priceFilters: state.Filter.searchPriceFilter,
        totalMatchingResults: state.Search.totalMatchingResults,
        lastUpdatedDateTimeStamp: state.Search.lastUpdatedDateTimeStamp
    };
    
}

const mapActionsToProps = dispatch => {
    return {
        onInitialize: (query) => dispatch(initialiseSearch(query))
    };
}

export default connect(mapStateToProps, mapActionsToProps)(ProductListingPage);