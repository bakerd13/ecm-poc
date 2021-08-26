import React from "react";

import "./OriginalPrice.css";

// TODO style component
class OriginalPrice extends React.Component {
    
    render() {

        return (
            <div className="originalPrice">
                Original: {this.props.item.originalPrice.maxSalePrice}
                <br />
            </div>
            );
    }
}

export default OriginalPrice;