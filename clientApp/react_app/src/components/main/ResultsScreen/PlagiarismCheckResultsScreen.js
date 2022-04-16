import React from "react";
import Link from "./Link/Link";

class PlagiarismCheckResultsScreen extends React.Component {
    createArrayOfLinks() {
        let matchesArr = this.props.data.matches;
        console.log(matchesArr);
        let linksArr = [];

        for (const item in matchesArr) {
            const { url, percent } = matchesArr[item];

            linksArr.push(<Link link={url} percent={percent} />);
        }

        return linksArr;
    }

    render() {
        let linksArr = this.createArrayOfLinks();
        console.log(linksArr);
        return (
            <div className="body__main">
                <div className="pl-check-results-header__body">
                    <p className="heading__pl-check-re">
                        The precentage of plagiarism in the text is
                    </p>
                </div>
                <div className="container__body">
                    <div className="plagiarism-percent__body">
                        <p className="percent__plagiarism-body">
                            {this.props.data.percent}
                        </p>
                    </div>
                </div>
                <hr className="border__plagiarism-body" />
                <div className="wrapper">
                    <div className="wrapper_center">
                        {linksArr}
                    </div>
                </div>
            </div>
        );
    }
}

export default PlagiarismCheckResultsScreen;