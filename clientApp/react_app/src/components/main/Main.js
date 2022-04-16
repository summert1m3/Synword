import React from "react";
import Loading from "./Loading/Loading"
import MainScreen from "./MainScreen/MainScreen";
import LoadingScreen from "./LoadingScreen/LoadingScreen";
import PlagiarismCheckResultsScreen 
from "./ResultsScreen/PlagiarismCheckResultsScreen";
import PlagiarismCheckService from "../../services/PlagiarismCheckService"

class Main extends React.Component {
    plagiarismCheckService = new PlagiarismCheckService();

    state = {
        text: '',
        currentScreen: undefined
    };

    mainScreen;
    loadingScreen;
    plagiarismCheckResultsScreen = <PlagiarismCheckResultsScreen />;

    constructor() {
        super();

        this.mainScreen = this.updateMainScreen(this.state.text.length);
        this.loadingScreen = <LoadingScreen />;

        this.state.currentScreen = this.plagiarismCheckResultsScreen;
    }

    updateMainScreen = (textLength) => {
        return (
            <MainScreen
                symbolCount={textLength}
                onTextChange={this.onTextChange}
                onPlagiarismCheck={this.onPlagiarismCheck} />
        );
    }

    onTextChange = (str) => {
        this.setState({
            text: str,
            currentScreen: this.updateMainScreen(str.length)
        });
    }

    onPlagiarismCheck = async () => {
        this.setState({
            currentScreen: this.loadingScreen
        });

        this.setState({
            currentScreen: this.plagiarismCheckResultsScreen
        });
    }

    render() {
        return (
            this.state.currentScreen
        );
    }
}

export default Main;