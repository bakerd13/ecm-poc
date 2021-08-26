import React from "react";
import ShoppingBag from "../shoppingbag/ShoppingBag";


// TODO add google analytics TrackGAEvent look at react-ga
// what my account and quickshop do.
// left here for reference
class QuickLinks extends React.Component {

    state = {
        isMobile: false
    }

    createMobileSearch = () => {
        let search = null;

        if (this.state.isMobile) {
            if (this.props.searchOpen) {
                search = (<div className="nx-icon nav-close-search" onClick={this.props.searchOpenHandler}></div>);
            }
            else {
                search = (<div className="nx-icon nav-search" onClick={this.props.searchOpenHandler}></div>);
            }
        }
    
        return search;
    }

    myAccountClickHandler = () => {
        //TrackGAEvent('vip_mvp_header', 'Tap / Click', 'vip_my_account_header', 0);
    }

    quickShopClickHandler = () => {
        //TrackGAEvent('vip_mvp_header', 'Tap / Click', 'vip_quickshop_header', 0)
    }

    render() {
        const classQuickText= this.state.isMobile ? "nav-mobile-quicklinks-text" : "nav-quicklinks-text";

        return (
            <ul className="quickLinks">
                <li>
                    <a href="true" onClick={this.myAccountClickHandler}>
                        <div className="nx-icon nav-myaccount"></div>
                        <div className={classQuickText}>
                            <span className="headerWords">My Account</span>
                        </div>
                    </a>
                </li>
                <li>
                    {this.createMobileSearch()}
                </li>
                <li>
                    <a href="http://dab.localhost.uat1.test/eoss/quickshop" onClick={this.quickShopClickHandler}>
                        <div className="nx-icon nav-quickshop"></div>
                        <div className={classQuickText}>
                            <span className="headerWords">Quickshop</span>
                        </div>
                    </a>
                </li>
                <li>
                    <ShoppingBag />
                </li>
            </ul>)
    }
}

export default QuickLinks;