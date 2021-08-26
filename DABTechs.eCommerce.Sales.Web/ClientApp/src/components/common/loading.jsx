import React from "react";

import "../styles/spinners.css";

const loading = () => {
    return (
        <div className="nx-spinner-container">
            <div className="nx-spinner-center">
                <span className="nx-spinner-dark oval">
                    <div className="please-wait">
                        <span className="please-wait">Please wait...</span>
                    </div>
                </span>
            </div>
        </div>
    );
}

const pageLoading = () => {
    return (
        <div className="nx-spinner-pagecontainer">
            <div className="nx-spinner-center">
                <span className="nx-spinner-dark oval">
                    <div className="please-wait">
                        <span className="please-wait">Please wait...</span>
                    </div>
                </span>
            </div>
        </div>
    );
}

export { loading as Loading, pageLoading as PageLoading };