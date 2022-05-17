import React from "react";
import MainScreen from "./MainScreen/MainScreen";
import LoadingScreen from "./LoadingScreen/LoadingScreen";
import PlagiarismCheckResultsScreen
    from "./ResultsScreen/PlagiarismCheckResults/PlagiarismCheckResultsScreen";
import SynwordApiService from "../../services/synwordApiService";
import { connect } from "react-redux";
import Header from "../Header/Header";
import { changeText } from "../../actions/actions";
import RephraseResultsScreen from "./ResultsScreen/RephraseResults/RephraseResultsScreen";

class Main extends React.Component {
    _synwordApi = new SynwordApiService();
    controller;

    state = {
        currentScreen: undefined
    };

    mainScreen;
    loadingScreen;

    constructor() {
        super();

        this.mainScreen = this.updateMainScreen();
        this.loadingScreen =
            <LoadingScreen
                moveToMainScreen={this.moveToMainScreen} />;

        this.state.currentScreen = this.updateMainScreen();
    }

    updateMainScreen = () => {
        return (
            <MainScreen
                onPlagiarismCheck={this.onPlagiarismCheck}
                onRephrase={this.onRephrase} />
        );
    }

    onPlagiarismCheck = async () => {
        this.controller = new AbortController();

        this.setState({
            currentScreen: this.loadingScreen
        });

        try {
            let data = await this._synwordApi
                .plagiarismCheck(this.props.text, this.controller.signal);

            this.setState({
                currentScreen: <PlagiarismCheckResultsScreen data={data}
                    moveToMainScreen=
                    {this.moveToMainScreen} />
            });
        } catch (e) {
            console.log(e);
        }
    }

    onRephrase = async () => {
        this.controller = new AbortController();

        this.setState({
            currentScreen: this.loadingScreen
        });

        try {
            let data = await this._synwordApi
                .rephrase(this.props.text, this.controller.signal);

            this.setState({
                currentScreen:
                    <RephraseResultsScreen
                        data={data}
                        moveToMainScreen={this.moveToMainScreen} />
            });
        } catch (e) {
            console.log(e);
        }
    }

    moveToMainScreen = () => {
        this.controller.abort();
        this.props.changeText("");
        this.setState({
            currentScreen: this.updateMainScreen()
        });
    }

    render() {
        return (
            <div>
                <Header />
                <main>
                    {this.state.currentScreen}
                </main>
            </div>
        );
    }
}

const mapStateToProps = ({ text }) => {
    return {
        text: text
    };
};

const mapDispatchToProps = {
    changeText
};

export default connect(mapStateToProps, mapDispatchToProps)(Main);