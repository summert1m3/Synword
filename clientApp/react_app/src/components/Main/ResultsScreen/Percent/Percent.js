import React from "react";

class Percent extends React.Component {

    pickBackgroundColor = (percent) => {
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
            percent
        } = this.props;

        return <div
            style={{
                backgroundColor: this.pickBackgroundColor(percent)
            }}
            className="outer_color">
            <div className="percent-container__body">
                <div className="plagiarism-percent__body">
                    <p className="percent__plagiarism-body">
                        {percent}%
                    </p>
                </div>
            </div>
        </div>
    }
}

export default Percent;