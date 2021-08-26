import React from "react";
import { LinkButton }  from "../../common/StyledComponents";
// TODO change anchor to button

const ViewLess = (props) => {
    return (
        <div>
            <LinkButton onClick={props.onClicked}>View Less</LinkButton>
        </div>);
}

export default ViewLess;