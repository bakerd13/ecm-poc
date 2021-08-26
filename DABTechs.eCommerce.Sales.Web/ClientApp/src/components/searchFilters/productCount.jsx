import React from "react";
import { connect } from "react-redux";
import * as actionTypes from "../../store/actions/actionTypes";
import "./styles/productCount.css";

// TODO complete clear all logic and change to hook
class ProductCount extends React.Component {

    clearAllHandler = (e) => {
        e.preventDefault();
        this.props.onClearAll();
    }

    render() {
        
        let title = this.props.totalResults + " " + this.props.productText;
        let clear = null;

        if (this.props.filterCount.length > 0) {
            clear = (<a className="Clear" href="true" onClick={this.clearAllHandler}>Clear All</a>);
        }

        return (<div className="ResultCount">
            <div className="Count">
                {title}
                {clear}
            </div>
        </div>)

    }
}

const mapStateToProps = state => {
    return {
        filterCount: state.Filter.selectedFilters
    };
}

const mapActionsToProps = dispatch => {
    return {
        onClearAll: () => dispatch({type: actionTypes.CLEARSELECTEDALLFILTER})
    };
    
}

export default connect(mapStateToProps,mapActionsToProps)(ProductCount);
