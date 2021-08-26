import React, { useState } from "react";
import Wrapper from "../common/Wrapper";
import { GetMoreLessAllControl} from "./components";
import Modal from "../../helpers/modal";
import Facet from "./facet";
import { FilterModal } from "./components";
import _ from "lodash";

const FacetList = (props) => {
    const [showMore , setShowMore] = useState(true);
    const [showModal , setShowModal] = useState(false);
    
    const showMoreHandler = (e) => {
        setShowMore(!showMore);
    }

    const showViewAllHandler = (e) => {
        setShowModal(true);
    }

    const closeShowViewAllModal = (e) => {
        setShowModal(false);
    }

    const setFilterModal = () => {
        if (showModal) {
            return (<React.Fragment>
                    {showModal} ? (<Modal>
                        <FilterModal filter={props.facets} onClose={closeShowViewAllModal}/>
                    </Modal>) : null
                </React.Fragment>);
        }

        return null;
    }

    const generateFacets = (facetItems) => {
        const modalControl = setFilterModal();
        let name = facetItems.name;
        const optionsCount = facetItems.filterItems != null ? facetItems.filterItems.length : 0;
        const mimlimit = 8;
        const maxlimit = 30;
        const handler = optionsCount > 30 ? showViewAllHandler : showMoreHandler;

        const moreOrLess = GetMoreLessAllControl(optionsCount, showMore, handler, mimlimit, maxlimit);

        const list = _.orderBy(facetItems.filterItems, ['count'], ['desc']).map((o, index) => {
            return (<Facet key={"facet" + index} name={name} option={o} indexOption={index} initiallyShow="8" showMore={showMore} />);
        });

        return (
            <Wrapper>
                <ul className="List">
                    {list}
                    {moreOrLess}
                </ul>
                {modalControl}
            </Wrapper>
        );
    }

    return generateFacets(props.facets);
}

export default FacetList;