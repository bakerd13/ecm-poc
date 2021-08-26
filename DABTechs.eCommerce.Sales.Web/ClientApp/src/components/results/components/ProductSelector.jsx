import React from "react";
import Dropdown from "react-dropdown";

import "./ProductSelector.css";

// TODO style component

class ProductSelector extends React.Component {

    state = {
        selectedSize: null,
        selectedValue: null
    }

    onSelectionChanged = (state) => {
        this.setState({ selectedSize: state.value });
    }

    addItemToBag = () => {
        // TODO get  rid of console log and add dispatch
        console.log("addItemToBag clicked");
    }

    render() {
        // TODO check claims for User.IsAllowed(SaleClaimTypes.AllowAddToBag

        const options = this.props.item.sizeAndPriceList.filter((sizeItem) => {
            if (sizeItem.size === null || sizeItem.sizeCode === null)
            {
                return false;
            }
            return true;
        })
        .map((sizesItem) => {
            return { value: sizesItem.sizeCode, label: sizesItem.size };
        });

        return (
            <div className="qty-add-btn-container">
                <div className="options">
                    <div className="size-select-container">
                        <Dropdown className="sizes" options={options} value={this.state.selectedSize} onChange={this.onSelectionChanged} placeholder="Size"/>
                     </div>
                </div>
                    <div className="add-container">
                        <div id="addtobagcontainer">
                            <p className="addtobag">
                            <input className="vip-button primary" name="top" value="Add" title="Add to Bag" onClick={this.addItemToBag} type="button" />
                            </p>
                        </div>
                    </div>
                </div>
            );
    }
}

export default ProductSelector;