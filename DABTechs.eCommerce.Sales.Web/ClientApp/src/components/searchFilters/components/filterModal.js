import React, { useState } from "react"; 

import "../styles/filterModal.css";
import "../../common/styles/Buttons.css";

// TODO style component check vip buttons
const FilterModal = (props) => {
    const [state, setState] = useState({
        A: false,
        B: false,
        C: false,
        D: false,
        E: false,
        F: false,
        G: false,
        H: false,
        I: false,
        J: false,
        K: false,
        L: false,
        M: false,
        N: false,
        O: false,
        P: false,
        Q: false,
        R: false,
        S: false,
        T: false,
        U: false,
        V: false,
        W: false,
        X: false,
        Y: false,
        Z: false,
        num: false
    });

        return (
            <div className="facets-popup-container show">
                <div className="filter-modal">
                    <div className="control-bar">
                        <div className="title-bar"></div>
                        <a className="close" onClick={props.onClose}>Close</a>
                        <div className="auto-dropdown"></div>
                    </div>
                    <div className="letter-nav"></div>
                    <div className="filter-section">
                        <div className="filter-modal-list"></div>
                    </div>
                    <div className="filter-modal-summary">
                        <div className="selected-title-bar">
                            <h3>THIS IS A MODAL</h3>
                            <a className="clear" href="" style={{display: "none"}}>Clear</a>
                        </div>
                        <ul></ul>
                    </div>
                    <div className="button-wrap">
                        <a className="vip-button secondary" href="">Confirm</a>
                    </div>
                </div>
            </div>
        );
}

export default FilterModal;