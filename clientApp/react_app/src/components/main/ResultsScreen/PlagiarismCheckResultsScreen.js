import React from "react";
import Link from "./Link/Link";

class PlagiarismCheckResultsScreen extends React.Component {
    render() {
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
                            50%
                        </p>
                    </div>
                </div>
                <hr className="border__plagiarism-body"/>
                <Link link="yandex.ru" percent="80%"/>
            </div>
        );
    }
}

export default PlagiarismCheckResultsScreen;