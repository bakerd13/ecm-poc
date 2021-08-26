import React from "react";

import "./WasNowPrice.css";

//TODO styles
class WasNowPrice extends React.Component {
    
    render() {
        // TODO what is the current price @Model.CurrentPrice
        return (
            <div className="was-now-container">
                <p className="prices">Now {this.props.item.currentPrice.minSalePrice}</p>
            </div>
            );
    }
}

export default WasNowPrice;