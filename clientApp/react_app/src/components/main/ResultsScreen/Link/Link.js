import React from "react";

class Link extends React.Component {
    render() {
        const {
            link,
            percent
        } = this.props;

        return (
            <div className="links__plagiarism-body">
                <a href={link}>{link}</a>
                <p className="percent__links">{percent}</p>
            </div>
        );
    }
}

export default Link;