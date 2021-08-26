import React, { useState, useEffect } from "react";
import { useDispatch } from "react-redux";
import { UpdateFacet } from "../../api/search";
import { LinkButton }  from "../common/StyledComponents";

import _ from "lodash";

import "./styles/facet.css";

// TODO change the way styles are done
// also need to think about debounce on this.props.onGetFilterdSearchPage(this.props.storeFilters)
const FacetHook = (props) => {
    const [selected, setSelected] = useState(false);

    // TODO this run on every first load 
    // useEffect(() => {
    //     updateResults();
    // }, [selected]);

    const dispatch = useDispatch();
    const onUpdated = dispatch((facetname, facetOption, checked) => UpdateFacet(facetname, facetOption, checked));

    const updateResults = (selected) => {
        console.log("updateResults ", selected);
        
        onUpdated(props.option.facetName, props.option.value, selected);
    }

    const selectedHandler = (e) => {
        setSelected(!selected);
    }

    let facetOptionClass = null;
    if (props.indexOption < props.initiallyShow && props.showMore) {
        facetOptionClass = "facet-option";
    }
    else if (props.showMore) {
        facetOptionClass = "facet-option facet-hide";
    }
    else {
        facetOptionClass = "facet-option";
    }

    const optionCount = _.concat("(", props.option.count, ")");
    const optionDisplayName = props.option.title.trim();

    let labelChild = null;

    labelChild = (
        <div>
            <LinkButton className="Label" title={optionDisplayName}
                onClick={selectedHandler}>
                <span className="option-name">{optionDisplayName}</span>
            </LinkButton>
            <span className="count">{optionCount}</span>
        </div>
    );

    return (
        <li className={facetOptionClass}>
            <div className="option">
                <input type="checkbox" className="opt"
                    value={props.option.value}
                    checked={selected}
                    onChange={selectedHandler} />
                <label>
                    {labelChild}
                </label>
            </div>
        </li>
    );
}

export default FacetHook;