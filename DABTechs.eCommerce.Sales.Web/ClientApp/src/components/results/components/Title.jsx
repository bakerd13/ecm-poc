import React from "react";

import "./Title.css";

// TODO Get Backup class name for title
class Title extends React.Component {
    
    render() {

        // TODO revist title       
        return (
            <div className="product-title-container">
                <p className="product-title">
                    <label>{this.props.item.title}</label>
                </p>
            </div>
            );
    }
}

export default Title;