import React from "react";
import Wrapper from "../common/Wrapper";

import "./ShoppingBagFooter.css";

// TODO make sure bag calculation removes from count unavailable items
// link to href needs to change to bag service
class ShoppingBagFooter extends React.Component {

    render() {
        const itemCount = this.props.shoppingBag.items !== null || this.props.shoppingBag.items !== undefined ? this.props.shoppingBag.items.length : 0;
        return (
            <Wrapper>
                <div class="bagFooter">
                    <div class="total">
                        <div class="left">Total</div>
                        <div class="right">£56.00</div>
                    </div>

                    <div class="subText">Excluding UK Standard Delivery (Normally £3.99)</div>
                    <a class="view_edit_bag" href="//www.next.co.uk/shoppingbag">View/Edit Bag</a>
                    <a class="checkout" href="//www.next.co.uk/secure/checkout/transfer/checkoutcta">CHECKOUT</a>
                </div>
            </Wrapper>
        )
    }
}


export default ShoppingBagFooter;