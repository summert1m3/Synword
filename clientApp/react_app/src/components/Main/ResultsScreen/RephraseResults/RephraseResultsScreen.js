import React from "react";
import './rephraseResultsScreen.css';

import HeaderResultsContainer from "../HeaderResults/HeaderResultsContainer";
import "../PlagiarismCheckResults/plagiarismCheckResultsScreen.css";
import RephrasedWordWrapper from "./RephrasedWordWrapper";

class RephraseResultsScreen extends React.Component {

    addHighlightsToText = (text, sourceWordSynonyms) => {
        let symbols = text.split("");

        let arr = [];
        let index = 0;

        for(let i = 0; i < sourceWordSynonyms.length; i++) {
            const {
                synonymWordStartIndex,
                synonymWordEndIndex,
            } = sourceWordSynonyms[i];

            arr.push(symbols.slice(index, synonymWordStartIndex));
            arr.push(<RephrasedWordWrapper
                        synonyms={sourceWordSynonyms[i]}/>);

            index = synonymWordEndIndex + 1;
        }

        return arr;
    }

    render() {
        const {
            moveToMainScreen
        } = this.props;

        const {
            rephrasedText,
            synonyms
        } = this.props.data;
        
        let textWithHighlights =
            this.addHighlightsToText(rephrasedText, synonyms);

        return (
            <div className="body__results__main">
                <HeaderResultsContainer
                    title="Результаты"
                    moveToMainScreen
                    ={moveToMainScreen} />

                <hr className="border__plagiarism-body" />

                <div className="textarea__results">
                    {textWithHighlights}
                </div>
            </div>
        );
    }
}

export default RephraseResultsScreen;