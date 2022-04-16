import React from "react";

class SymbolCount extends React.Component {
    render() {
        const {
            symbolCount
        } = this.props;

        return (
            <div className="symbol-count">
                {symbolCount}/20000
            </div>
        );
    }
}

export default SymbolCount;