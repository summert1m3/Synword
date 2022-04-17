import React from "react";

class Link extends React.Component {
    constructor(props) {
        super(props);
        this.state = { 
            windowWidth: window.innerWidth 
        };
    }

    handleResize = (e) => {
        this.setState({ windowWidth: window.innerWidth });
    };

    componentDidMount() {
        window.addEventListener("resize", this.handleResize);
    }

    componentWillUnmount() {
        window.addEventListener("resize", this.handleResize);
    }

    sliceLink(url, limit) {
        let sliced = url.slice(0, limit);

        if (sliced.length < url.length) {
            sliced += '...';
        }

        return sliced;
    }

    render() {
        const {
            url,
            percent
        } = this.props.match;

        let width = this.state.windowWidth;
        let sliced;

        if (width > 1044) {
            sliced = this.sliceLink(url, 60);
        }
        else if (width > 715) {
            sliced = this.sliceLink(url, 35);
        }
        else {
            sliced = this.sliceLink(url, 25);
        }

        return (
            <div className="links__plagiarism-body">
                <a href={url}>{sliced}</a>
                <div className="percent__links">
                    <p>{percent}%</p>
                    <button id="show_matches_button">
                    </button>
                </div>

            </div>
        );
    }
}

export default Link;