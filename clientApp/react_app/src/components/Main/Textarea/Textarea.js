import React from "react";
import { connect } from 'react-redux';
import { changeText } from '../../../actions/actions';

class Textarea extends React.Component {
    onTextChange = (event) => {
        this.props.changeText(event.target.value);
    }

    render() {
        return (
            <textarea
                onChange={this.onTextChange}
                placeholder="Пожалуйста, введите свой текст"
                className="textarea__body">
            </textarea>
        );
    }
}

const mapDispatchToProps = {
    changeText
};

export default connect(null, mapDispatchToProps)(Textarea);