import React from "react";
import Button from '@material-ui/core/Button';
import "./footerMain.css";

const FooterMain = ({onPlagiarismCheck}) => (
    <div className="bottom-area__main">
        <div className="check-plagiarism-button" >
            <Button onClick={onPlagiarismCheck} style={{
                fontFamily: 'Gardens',
                backgroundColor: "#DFC777",
                color: "black"
            }}
                color="primary"
                variant="contained">Проверить
            </Button>
        </div>
    </div>
);

export default FooterMain;