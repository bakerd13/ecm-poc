import React from "react";

import "./ProductSizes.css";

// TODO add styles
class ProductSizes extends React.Component {
    // TODO MoreOrLess function and @Model.GetAvailableSizes()
    render() {

        return (
            <div className="product-sizes-container">
                <p className="product-sizes-title">
                    <label>Sizes still available:</label>
                </p>
                <div className="product-sizes">{"@Model.GetAvailableSizes()"}</div>
            </div>
            );
    }
}

export default ProductSizes;