import React from "react";

const Header = ({title, onClosePlagiarismCheckResults}) => (
    <div className="pl-check-results-header__body">
        <p 
        dangerouslySetInnerHTML={{ __html: title }}
        className="heading__pl-check-re">
        </p>
        <button id="close_button"
            onClick={onClosePlagiarismCheckResults}>
        </button>
    </div>
);

export default Header;