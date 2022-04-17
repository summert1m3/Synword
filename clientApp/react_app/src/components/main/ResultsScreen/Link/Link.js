import React from "react";

class Link extends React.Component {
    constructor(props) {
        super(props);
        this.state = { windowWidth: window.innerWidth };
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

    sliceLink(link, limit) {
        let sliced = link.slice(0, limit);

        if (sliced.length < link.length) {
            sliced += '...';
        }

        return sliced;
    }

    render() {
        const {
            link,
            percent
        } = this.props;

        let width = this.state.windowWidth;
        let sliced;

        if (width > 1044) {
            sliced = this.sliceLink(link, 60);
        }
        else if (width > 715) {
            sliced = this.sliceLink(link, 35);
        }
        else {
            sliced = this.sliceLink(link, 25);
        }

        return (
            <div className="links__plagiarism-body">
                <a href={link}>{sliced}</a>
                <p className="percent__links">{percent}%</p>
            </div>
        );
    }
}

export default Link;