import React from "react";
import Link from "./Link/Link";
import HeaderResultsContainer from "./HeaderResults/HeaderResultsContainer";
import Percent from "./Percent/Percent";
import "./plagiarismCheckResultsScreen.css";

class PlagiarismCheckResultsScreen extends React.Component {
    state = {
        highlights: this.props.data.highlights,
        windowWidth: window.innerWidth
    };

    handleResize = (e) => {
        this.setState({ windowWidth: window.innerWidth });
    };

    componentDidMount() {
        window.addEventListener("resize", this.handleResize);
    }

    componentWillUnmount() {
        window.addEventListener("resize", this.handleResize);
    }

    createArrayOfLinks = (matchesArr) => {
        let linksArr = [];

        for (const item in matchesArr) {
            linksArr.push(<Link 
                windowWidth={this.state.windowWidth}
                match={matchesArr[item]}
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
            moveToMainScreen
        } = this.props;

        let linksArr = this.createArrayOfLinks(matches);

        let textWithHighlights =
            this.addHighlightsToText(text, this.state.highlights);

        return (
            <div className="body__results__main">
                <HeaderResultsContainer
                    windowWidth={this.state.windowWidth}
                    moveToMainScreen
                    ={moveToMainScreen} />

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