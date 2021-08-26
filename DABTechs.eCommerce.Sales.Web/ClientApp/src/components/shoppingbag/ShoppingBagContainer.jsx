import React from "react";
import { connect } from "react-redux";

import ShoppingBagHeader from "./ShoppingBagHeader";
import ShoppingBagItems from "./ShoppingBagItems";
import ShoppingBagFooter from "./ShoppingBagFooter";

import "./ShoppingBagContainer.css";

// TODO make sure bag calculation removes from count unavailable items
// link to href needs to change to bag service
class ShoppingBagContainer extends React.Component {

    render() {
        
        return (
            <div className="shopping-bag-container">
                <ShoppingBagHeader shoppingBag={this.props.bag} />
                <ShoppingBagItems shoppingBag={this.props.bag} />
                <ShoppingBagFooter shoppingBag={this.props.bag} />            
            </div>
        )
    }
}

const mapStateToProps = state => {
    return {
        bag: state.ShoppingBag
    };
    
}

export default connect(mapStateToProps, null)(ShoppingBagContainer);