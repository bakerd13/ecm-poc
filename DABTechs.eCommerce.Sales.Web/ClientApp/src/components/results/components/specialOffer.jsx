import React from "react";
//import "../search.css";

// TODO Get Backup class  style component

class SpecialOffer extends React.Component {
    
    render() {
        // TODO what is the current price @Model.CurrentPrice
        return (
            <div className="special-offer-container">
                <p className="special-offer">Special Offer</p>
                <p className="special-offer-price">Now {this.props.item.currentPrice.minSalePrice}</p>
            </div>
            );
    }
}

export default SpecialOffer;