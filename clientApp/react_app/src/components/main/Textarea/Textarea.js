import React from "react";

class Textarea extends React.Component {
    onTextChange = (event) => {
        this.props.onTextChange(event.target.value);
    }

    render() {
        return (
            <textarea
                onChange={this.onTextChange}
                placeholder="Please, input your text"
                className="textarea__body">
            </textarea>
        );
    }
}

export default Textarea;