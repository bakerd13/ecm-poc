import React from "react";

import "./ProductCode.css";

class ProductCode extends React.Component {
    
    render() {

        return (
            <div className="product-code">
                {this.props.item.productCode}
            </div>
            );
    }
}

export default ProductCode;