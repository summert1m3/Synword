import React from "react";
import Textarea from "../Textarea/Textarea";
import SymbolCount from "../Header/SymbolCount";
import FooterMain from "../Footer/FooterMain";

class MainScreen extends React.Component {
    render() {
        const {
            symbolCount,
            onTextChange,
            onPlagiarismCheck
        } = this.props;

        return (
            <div className="body__main">
                <SymbolCount symbolCount={symbolCount} />
                <Textarea onTextChange={onTextChange} />
                <FooterMain onPlagiarismCheck={onPlagiarismCheck} />
            </div>
        );
    }
}

export default MainScreen;