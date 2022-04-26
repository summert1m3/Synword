import React from "react";
import "./link.css";

class Link extends React.Component {
    sliceLink(url, limit) {
        let sliced = url.slice(0, limit);

        if (sliced.length < url.length) {
            sliced += '...';
        }

        return sliced;
    }

    changeHighlights = () => {
        this.props
            .changeHighlights(this.props.match.highlights);
    }

    render() {
        const {
            url,
            percent
        } = this.props.match;

        let width = this.props.windowWidth;
        let sliced;

        if (width > 1340) {
            sliced = this.sliceLink(url, 60);
        }
        else if (width > 1120) {
            sliced = this.sliceLink(url, 50);
        }
        else if (width > 842) {
            sliced = this.sliceLink(url, 35);
        }
        else if (width > 660) {
            sliced = this.sliceLink(url, 25);
        }
        else if (width > 580) {
            sliced = this.sliceLink(url, 20);
        }
        else {
            sliced = this.sliceLink(url, 15);
        }

        return (
            <div className="links__plagiarism-body">
                <a href={url}>{sliced}</a>
                <div className="percent__links">
                    <p>{percent}%</p>
                    <button
                        onClick={this.changeHighlights}
                        className="show_matches_button">
                    </button>
                </div>
            </div>
        );
    }
}

export default Link;