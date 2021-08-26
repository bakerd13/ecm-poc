import React from "react";
import { LinkButton }  from "../../common/StyledComponents";

export const ViewMore = (props) => {
    return (
        <div>
            <LinkButton onClick={props.onClicked}>View More</LinkButton>
        </div>);
}

export default ViewMore;