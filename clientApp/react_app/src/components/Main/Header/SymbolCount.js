import React from "react";
import { FilePicker } from "react-file-picker";

class SymbolCount extends React.Component {

      handleFileChange = file => {

      };

    render() {
        const {
            symbolCount
        } = this.props;

        return (
            <div className="header__body__main">
                <div className="symbol-count">
                    {symbolCount}/20000
                </div>
                <FilePicker
                    extensions={["docx"]}
                    onChange={this.handleFileChange}
                    onError={errMsg => console.log(errMsg)}>
                    <button
                        className="upload-file-button">
                    </button>
                </FilePicker>

            </div>
        );
    }
}

export default SymbolCount;