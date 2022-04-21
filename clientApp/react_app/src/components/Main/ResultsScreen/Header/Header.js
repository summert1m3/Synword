import React from "react";

const Header = ({title, onClosePlagiarismCheckResults}) => (
    <div className="pl-check-results-header__body">
        <p className="heading__pl-check-re">
            {title}
        </p>
        <button id="close_button"
            onClick={onClosePlagiarismCheckResults}>
        </button>
    </div>
);

export default Header;