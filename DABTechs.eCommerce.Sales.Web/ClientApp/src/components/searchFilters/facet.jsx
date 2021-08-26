import React from "react";
import { connect } from "react-redux";
import { UpdateFacet } from "../../api/search";
import { LinkButton }  from "../common/StyledComponents";
import _ from "lodash";
import "./styles/facet.css";

// TODO change the way styles are done started facethook but did work so using class at mo
// also need to think about debounce on this.props.onGetFilterdSearchPage(this.props.storeFilters)
// subscribing to facet causes performance issues
class Facet extends React.Component {
    state = {
        checked: false
    }

    updateResults = () => {
        this.props.onUpdated(this.props.option.facetName, this.props.option.value, this.state.checked);
    }

    selectedHandler = (e) => {
        e.preventDefault();       
        this.setState({checked: !this.state.checked}, this.updateResults);
    }

    render() {
        let facetOptionClass = null;
        if(this.props.indexOption < this.props.initiallyShow && this.props.showMore)
        {
            facetOptionClass = "facet-option";
        }
        else if(this.props.showMore) {
            facetOptionClass = "facet-option facet-hide";
        }
        else
        {
            facetOptionClass = "facet-option";
        }
         
        const optionCount = _.concat("(", this.props.option.count, ")");
        const optionDisplayName = this.props.option.title.trim();

        let labelChild = null;

        labelChild = (
            <div>
                <LinkButton className="Label" title={optionDisplayName}
                    onClick={this.selectedHandler}>
                    <span className="option-name">{optionDisplayName}</span>
                </LinkButton>
                <span className="count">{optionCount}</span>
            </div>
        );

        return (
            <li className={facetOptionClass}>
                <div className="option">
                    <input type="checkbox" className="opt"
                        value={this.props.option.value}
                        checked={this.state.checked}
                        onChange={this.selectedHandler} />
                    <label>
                        {labelChild}
                    </label>
                </div>
            </li>
        );
    }
}

const mapActionsToProps = dispatch => {
    return {
        onUpdated: (facetname, facetOption, checked) => dispatch(UpdateFacet(facetname, facetOption, checked))
    };
    
}

export default connect(null,mapActionsToProps)(Facet);