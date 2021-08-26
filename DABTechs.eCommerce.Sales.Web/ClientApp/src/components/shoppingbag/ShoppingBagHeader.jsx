import React from "react";

import "./ShoppingBagHeader.css";

// TODO make sure bag calculation removes from count unavailable items
// link to href needs to change to bag service
class ShoppingBagHeader extends React.Component {

    render() {
        const itemCount = this.props.shoppingBag.items !== null || this.props.shoppingBag.items !== undefined ? this.props.shoppingBag.items.length : 0;
        return (
            <div className="bagHeader">
                <div className="items_count">{itemCount} Items In Bag</div>
            </div>
        )
    }
}


export default ShoppingBagHeader;