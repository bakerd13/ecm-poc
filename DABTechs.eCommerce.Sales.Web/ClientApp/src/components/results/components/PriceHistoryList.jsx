import React from "react";
import Wrapper from "../../common/Wrapper";

import "./PriceHistoryList.css";

// TODO Get Backup class name for  history list

class PriceHistoryList extends React.Component {

    render() {
        // TODO check roles @if (User.IsAllowed(SaleClaimTypes.PriceHistory) && Model.HasPriceHistory)
        // also need to check lassName="price-history-table arrow_box"
        // priceItem.date.ToString("MMMM yyyy")
        let priceList = null;

        if (this.props.item.priceHistory !== null && this.props.item.priceHistory.length > 0) {

            const historyPrices = this.props.item.priceHistory.map((p, index) => {
                return (
                    <tr key={index}>
                        <td className="left-align">p.date</td>
                        <td className="right-align">p.price</td>
                    </tr>
                );
            });

            priceList = (
                <Wrapper>
                    <div className="priceHistory">
                        <span>Price history</span>
                    </div>
                    <div id="priceHistoryToggle-{this.props.item.itemNo}" className="price-history-table arrow_box">
                        <div className="price-history-table-container">
                            <p>Price History:</p>
                            <div className="price-history-close-btn"> </div>
                            <table cellspacing="1">
                                <tbody>
                                    <tr>
                                        <th className="left-align">Date</th>
                                        <th className="right-align">Price</th>
                                    </tr>

                                    {historyPrices}

                                </tbody>
                            </table>
                        </div>
                    </div>
                </Wrapper>
            );
        }

        return (
            <div>
                {priceList}
            </div>
        );
    }
}

export default PriceHistoryList;