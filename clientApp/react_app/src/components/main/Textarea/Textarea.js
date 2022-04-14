import React from "react";

class Textarea extends React.Component {
    state = {
        text: ''
    };

    onTextChange = (event) => {
        this.setState({
            text: event.target.value
        });
        this.props.onSymbolCountChange(this.state.text.length);
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