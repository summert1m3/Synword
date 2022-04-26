import React from "react";
import Textarea from "../Textarea/Textarea";
import SymbolCount from "../Header/SymbolCount";
import FooterMain from "../Footer/FooterMain";
import Header from "../../Header/Header";

class MainScreen extends React.Component {
    render() {
        const {
            onPlagiarismCheck,
        } = this.props;

        return (
            <div className="body__main">
                <SymbolCount />
                <Textarea />
                <FooterMain onPlagiarismCheck={onPlagiarismCheck} />
            </div>
        );
    }
}

export default MainScreen;