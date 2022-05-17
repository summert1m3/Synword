import React from "react";
import "./rephraseResultsScreen.css";
import RephrasedWord from "./RephrasedWord";

class RephrasedWordWrapper extends React.Component {
    state = {
        word: this.props.synonyms.synonyms[0].value
    };

    changeWord = (word) => {
        this.setState({
            word: word
        });
    }

    render(){
        return(
            <RephrasedWord 
                currentWord={this.state.word}
                synonyms={this.props.synonyms}
                changeWord={this.changeWord}/>
        );
    }
}

export default RephrasedWordWrapper;