import React from "react";
import { LinkButton }  from "../../common/StyledComponents";
// TODO change anchor to button

const ViewAll = (props) => {
    return (
        <div>
            <LinkButton onClick={props.onClicked}>View All</LinkButton>
        </div>);
}

export default ViewAll;