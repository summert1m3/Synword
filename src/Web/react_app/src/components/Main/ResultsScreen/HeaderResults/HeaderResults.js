import React from "react";
import "./headerResults.css";

const HeaderResults = ({title, moveToMainScreen}) => (
    <div className="pl-check-results-header__body">
        <p 
        dangerouslySetInnerHTML={{ __html: title }}
        className="heading__pl-check-re">
        </p>
        <button id="close_button"
            onClick={moveToMainScreen}>
        </button>
    </div>
);

export default HeaderResults;