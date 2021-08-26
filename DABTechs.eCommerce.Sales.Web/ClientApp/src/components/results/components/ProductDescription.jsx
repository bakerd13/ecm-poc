import React from "react";

import "./ProductDescription.css";

// TODO add product description classes
class ProductDescription extends React.Component {
    
    render() {
        // TODO MoreOrLess function
        return (
            <div className="product-description">
                {this.props.item.composition}
            </div>
            );
    }
}

export default ProductDescription;