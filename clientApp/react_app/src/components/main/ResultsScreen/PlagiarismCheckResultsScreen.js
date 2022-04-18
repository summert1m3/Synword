import React from "react";
import Link from "./Link/Link";

class PlagiarismCheckResultsScreen extends React.Component {

    state = {
        highlights: this.props.data.highlights
    };

    createArrayOfLinks = () => {
        let matchesArr = this.props.data.matches;
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

    pickBackgroundColor() {
        let percent = this.props.data.percent;
        let color;

        switch (percent) {
            case (percent >= 50.0): color = "greenyellow"; break;
            case (percent >= 30.0): color = "yellow"; break;
            case (percent >= 0.0): color = "red"; break;
        }

        if (percent >= 50.0) {
            color = "greenyellow";
        }
        else if (percent >= 30.0) {
            color = "yellow";
        }
        else if (percent >= 0.0) {
            color = "red";
        }

        return color;
    }

    render() {
        const {
            text
        } = this.props.data;

        let linksArr = this.createArrayOfLinks();

        let textWithHighlights =
            this.addHighlightsToText(text, this.state.highlights);

        return (
            <div className="body__results__main">
                <div className="pl-check-results-header__body">
                    <p className="heading__pl-check-re">
                        Уникальность текста равна
                    </p>
                    <button id="close_button"
                        onClick={this.props.onClosePlagiarismCheckResults}>
                    </button>
                </div>
                <div className="container_1">
                    <div
                        style={{ backgroundColor: this.pickBackgroundColor() }}
                        className="outer_color">
                        <div className="container__body">
                            <div className="plagiarism-percent__body">
                                <p className="percent__plagiarism-body">
                                    {this.props.data.percent}%
                                </p>
                            </div>
                        </div>
                    </div>
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