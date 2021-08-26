import React from "react";
import SpecialOffer from "./specialOffer";
import OriginalPrice from "./OriginalPrice";
import WasNowPrice from "./WasNowPrice";
import PriceHistoryList from "./PriceHistoryList";

import "./ProductPriceHistory.css";

// TODO Get Backup class name for history
class ProductPriceHistory extends React.Component {
    
    render() {
        return (
            <div className="price-history-container">
                <SpecialOffer item={this.props.item} />
                <div className="tooltip">
                    <OriginalPrice item={this.props.item} />
                    <WasNowPrice item={this.props.item} />
                    <PriceHistoryList item={this.props.item} />
                </div>
            </div>
            );
    }
}

export default ProductPriceHistory;