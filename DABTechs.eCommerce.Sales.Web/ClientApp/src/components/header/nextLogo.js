import React from "react";
import "./Logo.css";

const classes = {
    homelink: {
        background: "#111111 url('data:image/png;base64,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') center top no-repeat",
        minWidth: "100px",
        height: "28px",
        float: "left",
        backgroundPosition: "left",
        margin: "0 0 0 11px",
    }
}

// TODO add link to site was "http://dab.localhost.uat1.test/" which come from config
// also could change this to a normal function component
const NextLogo = () => {
        
        return (
            <div className="logo">
                <a href="/" className="homelink">
                        <span>Go to the Next homepage for clothes, homeware &amp; more</span>
                </a>
            </div>
        );
}

export default NextLogo;