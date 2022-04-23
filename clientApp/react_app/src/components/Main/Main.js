import React from "react";
import MainScreen from "./MainScreen/MainScreen";
import LoadingScreen from "./LoadingScreen/LoadingScreen";
import PlagiarismCheckResultsScreen
    from "./ResultsScreen/PlagiarismCheckResultsScreen";
import PlagiarismCheckService from "../../services/plagiarismCheckService"

class Main extends React.Component {
    plagiarismCheckService = new PlagiarismCheckService();
    controller;

    state = {
        text: '',
        currentScreen: undefined
    };

    mainScreen;
    loadingScreen;

    constructor() {
        super();

        this.mainScreen = this.updateMainScreen(this.state.text.length);
        this.loadingScreen = <LoadingScreen onClose={this.onClose} />;

        this.state.currentScreen = this.mainScreen;
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
        this.controller = new AbortController();

        this.setState({
            currentScreen: this.loadingScreen
        });

        try {
            let data = await this.plagiarismCheckService
                .plagiarismCheck(this.state.text, this.controller.signal);

            this.setState({
                currentScreen: <PlagiarismCheckResultsScreen data={data}
                    onClose=
                    {this.onClose} />
            });
        } catch (e) {
            console.log(e);
        }
    }

    onClose = () => {
        this.controller.abort();
        this.setState({
            currentScreen: this.updateMainScreen(0)
        });
    }

    render() {
        return (
            this.state.currentScreen
        );
    }
}

export default Main;