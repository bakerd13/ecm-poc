import React from "react";

import "../Product.css";

// TODO Get Backup class name for image

class ItemImage extends React.Component {
    render() { 

        // TODO Get image properly
        let discount = null;

         // TODO @if (User.IsAllowed(SaleClaimTypes.OverlayImageSavings))
        if (true) {
            discount = (<div className="DiscountClass"></div>);
        }

        return (
            <div className="product-img-container">
                <img src={this.props.item.imageUrl} alt={this.props.item.title} />
                {discount}
            </div>
        );
    }
}

export default ItemImage;