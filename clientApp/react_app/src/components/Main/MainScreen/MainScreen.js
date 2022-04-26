import React from "react";
import Textarea from "./Textarea/Textarea";
import HeaderMain from "./HeaderMain/HeaderMain";
import FooterMain from "./FooterMain/FooterMain";
import "./mainScreen.css";

class MainScreen extends React.Component {
    render() {
        const {
            onPlagiarismCheck,
        } = this.props;

        return (
            <div className="body__main">
                <HeaderMain />
                <Textarea />
                <FooterMain onPlagiarismCheck={onPlagiarismCheck} />
            </div>
        );
    }
}

export default MainScreen;