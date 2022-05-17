import React from "react";
import Loading from "./Loading/Loading";

class LoadingScreen extends React.Component {
    render() {
        return (
            <div className="body__main">
                <Loading onClose={this.props.moveToMainScreen}/>
            </div>
        );
    }
}

export default LoadingScreen;