import React from "react";
import Loading from "../Loading/Loading";

class LoadingScreen extends React.Component {
    render() {
        return (
            <div className="body__main">
                <Loading />
            </div>
        );
    }
}

export default LoadingScreen;