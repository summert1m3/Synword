import React from "react";
import Link from "./Link/Link";

class PlagiarismCheckResultsScreen extends React.Component {
    createArrayOfLinks() {
        let matchesArr = this.props.data.matches;
        let linksArr = [];

        for (const item in matchesArr) {
            const { url, percent } = matchesArr[item];

            linksArr.push(<Link link={url} percent={percent} />);
        }

        return linksArr;
    }

    pickBackgroundColor() {
        console.log("123");
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
        let linksArr = this.createArrayOfLinks();
        return (
            <div className="body__main">
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
                <div className="wrapper">
                    <div>
                        {linksArr}
                    </div>
                </div>
                <textarea
                    value={this.props.data.text}
                    readOnly
                    className="textarea__results">
                </textarea>
            </div>
        );
    }
}

export default PlagiarismCheckResultsScreen;