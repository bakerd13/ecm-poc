import React from "react";
import { connect } from "react-redux";
import { Range } from "rc-slider";
import "rc-slider/assets/index.css";
import { applyPriceChanges } from "../../api/search/searchApi";

import "./styles/price.css";

// TODO add range and price labels
class Price extends React.Component {

    state = {
        priceFrom: this.props.priceFilters.priceFrom === 0 ? this.props.priceFilters.priceMin : this.props.priceFilters.priceFrom,
        priceTo: this.props.priceFilters.priceTo === 0 ? this.props.priceFilters.priceMax : this.props.priceFilters.priceTo,
        priceMin: this.props.priceFilters.priceMin,
        priceMax: this.props.priceFilters.priceMax
    }

    SliderChangeHandler = (prices) => {
        this.setState({priceFrom: prices[0], priceTo: prices[1]})
    }

    SliderAfterChangeHandler = (e) => {
        this.props.applyPriceChanges(this.state.priceFrom, this.state.priceTo);
    }

    render() {
        
        return (
            <div className="PriceFilter">
                <div className="PriceRangeDisplay">
                    <span>Price Range:</span><span className="CurrentRange" />
                </div>
                <div className="slider">
                    <Range 
                        min={this.state.priceMin} 
                        max={this.state.priceMax} 
                        defaultValue={[this.state.priceFrom, this.state.priceTo]} 
                        onChange={this.SliderChangeHandler} 
                        onAfterChange={this.SliderAfterChangeHandler}/>
                </div>
            </div>
        );
    }
}

const mapStateToProps = state => {
    return {
        priceFilters: state.Filter.searchPriceFilter
    }; 
}

const mapActionsToProps = dispatch => {
    return {
        applyPriceChanges: (from, to) => dispatch(applyPriceChanges(from, to))
    };   
}

export default connect(mapStateToProps, mapActionsToProps)(Price);