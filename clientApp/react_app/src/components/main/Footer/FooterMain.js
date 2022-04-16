import React from "react";
import Button from '@material-ui/core/Button';

const FooterMain = ({onPlagiarismCheck}) => (
    <div className="bottom-area__main">
        <div className="check-plagiarism-button" >
            <Button onClick={onPlagiarismCheck} style={{
                backgroundColor: "#DFC777",
                color: "black"
            }}
                color="primary"
                variant="contained">Check Plagiarism
            </Button>
        </div>
    </div>
);

export default FooterMain;