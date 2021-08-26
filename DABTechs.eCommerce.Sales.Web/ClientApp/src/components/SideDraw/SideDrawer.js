import React from "react";

import "./SideDrawer.css";

const sideDrawer = props => {
    const sideDrawerClasses = props.show ? "side-drawer open" : "side-drawer";

    return (
    <nav className={sideDrawerClasses}>
        <ul>
            <li>TODO</li>
        </ul>
    </nav>);
}

export default sideDrawer;