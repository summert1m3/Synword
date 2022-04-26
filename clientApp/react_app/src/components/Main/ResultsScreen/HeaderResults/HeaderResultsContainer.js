import React from "react";
import HeaderResults from "./HeaderResults";

class HeaderResultsContainer extends React.Component {
    render() {
        const {
            windowWidth,
            moveToMainScreen
        } = this.props;

        let title;
        
        if(windowWidth >= 550) {
            title = "Уникальность текста равна";
        }
        else {
            title = "Уникальность текста<br>равна"
        }
        return (
            <HeaderResults 
            title={title}
            moveToMainScreen
            ={moveToMainScreen}/>
        );
    }
}

export default HeaderResultsContainer;