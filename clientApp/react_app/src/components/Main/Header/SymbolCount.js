import React from "react";
import { FilePicker } from "react-file-picker";
import { connect } from 'react-redux';

class SymbolCount extends React.Component {

    handleFileChange = file => {

    };

    render() {
        const {
            textLength
        } = this.props;

        return (
            <div className="header__body__main">
                <div className="symbol-count">
                    {textLength}/20000
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

const mapStateToProps = ({ text }) => {
    return { 
        textLength: text.length
    };
};

export default connect(mapStateToProps, null)(SymbolCount);