import React from "react";
import Button from '@material-ui/core/Button';
import "./footerMain.css";

const FooterMain = ({onPlagiarismCheck, onRephrase}) => (
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
            <div className="block">

            </div>
            <Button onClick={onRephrase} style={{
                fontFamily: 'Gardens',
                backgroundColor: "#D78E89",
                color: "black"
            }}
                color="primary"
                variant="contained">Повысить
            </Button>
        </div>
    </div>
);

export default FooterMain;