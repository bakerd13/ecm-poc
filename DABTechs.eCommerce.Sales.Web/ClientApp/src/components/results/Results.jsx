import React from "react";
import { connect } from "react-redux";
import InfiniteScroll from 'react-infinite-scroller';
import StackGrid from 'react-stack-grid';
import { getSearchPage } from "../../api/search/searchApi";
import Product from "./Product";
//import ProductCard from "./ProductCard";
import { PageLoading } from "../common/loading";
import * as constants from "../common/constants";

// TODO check paging on api

const gridStyle={
    height: '100%',
    overflow: 'hidden',
    margin: 0,
    padding: 0
};

class Results extends React.Component {

    state = {
        mounted: false,
        hasMore: true,
        initialLoad: false
    };

    componentDidMount() {
        this.setState({ mounted: true });
    }


    loadPage = (pageNum) => {
        if ((this.props.searchResults[pageNum - 1] === null &&
            this.props.searchResults[pageNum - 1] === undefined) ||
            this.props.searchResults[pageNum - 1].results.length < constants.MaxResultsPerPage) {
            this.setState({ hasMore: false });
        }
        else {
            this.props.onGetSearchPage(pageNum + 1);
        }
    }

    render() {
        console.log("Results render",this.props.searchResults);
        let items = this.props.searchResults.map((page, index) => {
            return page.results.map((item, index) => {
                return (<div key={index}>
                    <Product item={item} />
                </div>);
            })
        });

        return (
            <div style={gridStyle} ref={(ref) => this.scrollParentRef = ref}>
                <InfiniteScroll
                    pageStart={0}
                    loadMore={this.loadPage.bind(this)}
                    hasMore={this.state.hasMore}
                    loader={<PageLoading  key={0} />}
                    useWindow={true}
                    initialLoad={this.state.initialLoad}                
                >
                    <StackGrid style={gridStyle} columnWidth={350} gutterWidth={1} gutterHeight={1}>
                        {items}
                    </StackGrid>
                </InfiniteScroll>
            </div>
        );
    }
}


const mapActionsToProps = dispatch => {
    return {
        onGetSearchPage: (page) => dispatch(getSearchPage(page))
    };
    
}

export default connect(null,mapActionsToProps)(Results);
