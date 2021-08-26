import React from "react"; 
import ProductCount from "./productCount";
import FilterControls from "./filterControls";
import "./styles/filterControls.css";

// TODO check filters2 class name and values
const Filters = (props) => {
    return (
        <div className="filters">
            <ProductCount totalResults={props.totalResults} productText={props.productText} />
            <FilterControls searchFilters={props.searchFilters} priceFilters={props.priceFilters} />
        </div>
    )
}

export default Filters;