import React from "react";
import { withRouter } from "react-router-dom";
import Wrapper from "../common/Wrapper";
import LinkButton from "../common/StyledComponents/linkButton";
import Grid from "@material-ui/core/Grid";
import { SearchUrl } from "../../config/constants";

class SubMenu extends React.Component {

    selectedLinkHandler = (e) => {
        this.props.onLinkClick();
        this.props.history.push(SearchUrl + "?" + e.currentTarget.value);
    }

    createSunMenus = (department) => {
        // TODO department.name could have spaces and special chars will not help key names
        if (department.searchLinks)
        {
            const columns = department.searchLinks[0].columns;
        
            let subMenu = columns.map((col, index) => {
                const sections = col.sections.map((sec, index) => {
                    const links = sec.links.map((link, index) => {
                        return (<div key={department.name +"_menulink_" + index} className="submenu-category-item">
                            <LinkButton className="menu-link" value={link.siteUrl}
                                onClick={this.selectedLinkHandler}>{link.title}</LinkButton>
                        </div>)
                    });
                    return (<Grid item direction="column" key={department.name + "_menusection_" + index}>
                        <div key={department.name + "_menusection-label" + index} className="submenu-category-item">
                            <label className="section">{sec.title}</label>
                        </div>
                        {links}
                    </Grid>);
                });
                return (
                <Grid key={department.name + "_menucolumn_" + index} className="submenu-sections" onClick={this.subMenuClickHanlder}>{sections}</Grid>
                );
            }); 
    
            return (
                <Grid container spacing={1} direction="row" justify="flex-start" alignItems="flex-start">
                    {subMenu}
                </Grid>
            );
        }
        else{
            return null;
        }
        
    }

    render() {
        return (<Wrapper>
            {this.createSunMenus(this.props.department)}
        </Wrapper>);
    }
}

export default withRouter(SubMenu);