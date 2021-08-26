import React from "react";
import { connect } from "react-redux";
import Wrapper from "../common/Wrapper";
import IconButton from '@material-ui/core/IconButton';
import ShoppingCartIcon from '@material-ui/icons/ShoppingCart';
import Badge from '@material-ui/core/Badge';
import Drawer from "@material-ui/core/Drawer";
import ShoppingBagContainer from "./ShoppingBagContainer";

// TODO make sure bag calculation removes from count unavailable items
// style the badge content override color

class ShoppingBag extends React.Component {

    state = {
        showBag: false
    }

    showBagHandler = () => {
        this.setState((prevState) => {
            return {showBag: !prevState.showBag};
        });
    }

    toggleDrawer = (event) => {
        if (event.type === "keydown" && (event.key === "Tab" || event.key === "Shift"))
        {
            return;
        }

        this.setState({showBag: !this.state.showBag});
    }

    render() {
        
        let bagCount = 0;
        if(this.props.bag.items !== null || this.props.bag.itemd !== undefined) {
            bagCount = this.props.bag.items.length;
        }

        return (
            <Wrapper>
                <IconButton
                    edge="end"
                    aria-label="View your shopping bag"
                    aria-controls="primary-shopping-bag-menu"
                    aria-haspopup="true"
                    color="inherit"
                    onClick={this.showBagHandler}
                >
                    <Badge badgeContent={bagCount} color="secondary">
                        <ShoppingCartIcon />
                    </Badge>
                </IconButton>

                <Drawer anchor="right" open={this.state.showBag} onClose={this.toggleDrawer}>
                    <ShoppingBagContainer />
                </Drawer>
            </Wrapper>
        )
    }
}

const mapStateToProps = state => {
    return {
        bag: state.ShoppingBag
    };
    
}

export default connect(mapStateToProps, null)(ShoppingBag);