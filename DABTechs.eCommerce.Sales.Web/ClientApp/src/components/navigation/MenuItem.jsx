import React from "react";
import { withRouter } from "react-router-dom";
import { withStyles, ThemeProvider } from "@material-ui/core/styles";
import Button from "@material-ui/core/Button";
import Menu from "@material-ui/core/Menu";

import { SearchUrl } from "../../config/constants";
import SubMenu from "./SubMenu";

//import "./Menu.css";
//import "./MenuItem.css";

const MenuButton = withStyles({
    root: {
      boxShadow: 'none',
      textTransform: 'none',
      fontSize: 16,
      padding: '6px 12px',
      border: 'none',
      lineHeight: 1.5,
      backgroundColor: 'transparent',
      color: 'white',
      fontFamily: [
        '-apple-system',
        'BlinkMacSystemFont',
        '"Segoe UI"',
        'Roboto',
        '"Helvetica Neue"',
        'Arial',
        'sans-serif',
        '"Apple Color Emoji"',
        '"Segoe UI Emoji"',
        '"Segoe UI Symbol"',
      ].join(','),
      '&:hover': {
        backgroundColor: 'black',
        color: 'white',
        borderColor: 'none',
        boxShadow: 'none',
      },
    },
  })(Button);

  const styles = (theme) => (
    {
        popoverPaper: {
            width: "90%",
            height: "80%",
            maxHeight: "unset",
            left: "5% !important"
        },
    }
);

// TODO need to make sure that the menu is corectly created and fall back and if no items do not create sub menus
class MenuItem extends React.Component {
    state = {
        anchorEl: null
    }
    
    selectedLinkHandler = (e) => {
        this.setState({anchorEl: null});
        this.props.history.push(SearchUrl + "?" + e.currentTarget.value);
    }

    onMouseEnterHandler = (e) => {
        this.setState({anchorEl: e.currentTarget});
    }

    handleClose = () => {
        this.setState({anchorEl: null});
    }
    
    render() {
        return (
            <div className="submenu-sections">
                <MenuButton
                    value={this.props.department.siteUrl}
                    onMouseEnter={this.onMouseEnterHandler}
                    onClick={this.selectedLinkHandler} 
                    aria-controls="vip-menu"
                    aria-haspopup="true"
                    variant="text"
                    size="large"
                >
                    {this.props.department.name}
                </MenuButton>
                {this.props.department.searchLinks ? 
                <Menu
                    id="vip-menu"
                    PopoverClasses={{ paper: this.props.classes.popoverPaper }}
                    anchorEl={this.state.anchorEl}
                    anchorOrigin={{ vertical: "bottom", horizontal: "center" }}
                    transformOrigin={{ vertical: "top", horizontal: "center" }}
                    getContentAnchorEl={null}
                    open={Boolean(this.state.anchorEl)}
                    onClose={this.handleClose}
                >
                    <SubMenu  department={this.props.department} onLinkClick={this.handleClose}/> 
                </Menu>
                : null}
            </div>
        );
    }
}

export default withStyles(styles)(withRouter(MenuItem));