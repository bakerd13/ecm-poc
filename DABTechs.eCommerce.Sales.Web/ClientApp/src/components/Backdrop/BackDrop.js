import React from "react";

import "./BackDrop.css";

const backdrop = props => {
    const classes = props.content ? "content-backdrop" : "main-backdrop";
    
    return (<div className={classes} onClick={props.closehandler}></div>);
};

export default backdrop;