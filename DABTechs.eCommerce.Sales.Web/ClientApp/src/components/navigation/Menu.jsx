import React from "react";
import { connect } from "react-redux";
import { initialiseMenu } from "../../api/menu/menuApi";

import MenuItem from "./MenuItem";

import "./Menu.css";

// TODO need to make sure that the menu is corectly created and fall back
// if no data produce a default menu
import data from "../../store/defaultMenu.json";

class Menu extends React.Component {

    componentDidMount() {
        this.props.onInitialize();
    }

    render() {

        const menuitems = this.props.menu.hasOwnProperty("departments") ? this.props.menu : data;

        const menu = menuitems.departments.map((dep, index) => {
            return (
                <MenuItem key={"menuitem_" + index} department={dep} backdropOpen={this.props.backdropOpen} backdropClickHandler={this.props.backdropClickHandler}/>
            );
        });

        return (<nav>
            <ul className="navbar">
                {menu}
            </ul>
        </nav>);
    }
}

const mapStateToProps = state => {
    return {
        menu: state.Menu.menu
    };
    
}

const mapActionsToProps = dispatch => {
    return {
        onInitialize: () => dispatch(initialiseMenu())
    };
    
}

export default connect(mapStateToProps, mapActionsToProps)(Menu);