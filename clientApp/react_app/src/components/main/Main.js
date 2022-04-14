import React from "react";
import Form from 'react-bootstrap/Form'
import Button from '@material-ui/core/Button';
import Textarea from "./Textarea/Textarea";
import SymbolCount from "./Header/SymbolCount";

class Main extends React.Component {
    state = {
        text: ''
    };

    onTextChange = (str) => {
        this.setState({
            text: str
        });
    }

    render() {
        return (
            <main>
                <div className="body__main">
                    <SymbolCount symbolCount={this.state.text.length}/>
                    <Textarea onTextChange={this.onTextChange}/>
                    <div className="bottom-area__main">
                        <div className="check-plagiarism-button" >
                            <Button
                                color="primary"
                                variant="contained">Check Plagiarism
                            </Button>
                        </div>
                    </div>
                </div>
            </main>
        );
    }
}

export default Main;