import React from "react";
import Wrapper from "../common/Wrapper";
import Group from "./group";
import FacetList from "./facetList";
import _ from "lodash";
import Price from "./price";

// TODO should this just be a function
import "./styles/filterControls.css";

const FilterControls = (props) => {

    const generateGroups = () => {
        return _.map(props.searchFilters, (facet, index) => {
            return (
                <Group key={"facetGroup" + index} name={facet.name} >
                    <FacetList facets={facet} />
                </Group>
            );
        });
    };

    return (
        <Wrapper>        
                {generateGroups()}
                <Group name="Price Range">
                    <Price filter={props.filter} />
                </Group>
        </Wrapper>
    )
}

export default FilterControls;