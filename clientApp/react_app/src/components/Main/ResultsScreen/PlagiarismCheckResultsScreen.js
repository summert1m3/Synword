import React from "react";
import Link from "./Link/Link";
import Header from "./Header/Header";
import Percent from "./Percent/Percent";

class PlagiarismCheckResultsScreen extends React.Component {

    state = {
        highlights: this.props.data.highlights
    };

    createArrayOfLinks = (matchesArr) => {
        let linksArr = [];

        for (const item in matchesArr) {
            linksArr.push(<Link match={matchesArr[item]}
                changeHighlights={this.changeHighlights} />);
        }

        return linksArr;
    }

    addHighlightsToText = (text, highlights) => {
        var words = text.split(" ");

        for (var i = 0; i < highlights.length; i++) {
            const {
                startIndex,
                endIndex
            } = highlights[i];

            words[startIndex] = '<b>' + words[startIndex];

            words[endIndex] = words[endIndex] + '</b>';
        }

        return words.join(" ");
    }

    changeHighlights = (highlights) => {
        this.setState({
            highlights: highlights
        });
    }

    render() {
        const {
            text,
            percent,
            matches
        } = this.props.data;

        const {
            onClosePlagiarismCheckResults
        } = this.props;

        let linksArr = this.createArrayOfLinks(matches);

        let textWithHighlights =
            this.addHighlightsToText(text, this.state.highlights);

        return (
            <div className="body__results__main">
                <Header
                    title="Уникальность текста равна"
                    onClosePlagiarismCheckResults
                    ={onClosePlagiarismCheckResults} />

                <div className="container_1">
                    <Percent percent={percent} />
                </div>

                <hr className="border__plagiarism-body" />

                <div className="wrapper_links">
                    <div className="wrapper_links_percents">
                        {linksArr}
                    </div>
                </div>

                <div className="source-text__results">
                    <p>
                        Исходный текст
                    </p>
                </div>

                <div className="textarea__results">
                    <p dangerouslySetInnerHTML={{ __html: textWithHighlights }}>
                    </p>
                </div>
            </div>
        );
    }
}

export default PlagiarismCheckResultsScreen;