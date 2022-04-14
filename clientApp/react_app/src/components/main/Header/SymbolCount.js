import React from "react";

class SymbolCount extends React.Component {
    render() {
        const {
            symbolCount
        } = this.props;

        return (
            <div className="symbol-count">
                Symbols: {symbolCount}
            </div>
        );
    }
}

export default SymbolCount;