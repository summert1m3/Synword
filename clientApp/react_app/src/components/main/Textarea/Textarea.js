import React from "react";

class Textarea extends React.Component {
    render() {
        return (
            <textarea
                placeholder="Please, input your text"
                className="textarea__body">
            </textarea>
        );
    }
}

export default Textarea;