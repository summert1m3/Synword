import React from "react";
import HeaderResults from "./HeaderResults";

class HeaderResultsContainer extends React.Component {
    render() {
        const {
            title,
            moveToMainScreen
        } = this.props;

        return (
            <HeaderResults 
            title={title}
            moveToMainScreen
            ={moveToMainScreen}/>
        );
    }
}

export default HeaderResultsContainer;