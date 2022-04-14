import React from "react";
import Form from 'react-bootstrap/Form'
import Button from '@material-ui/core/Button';
import Textarea from "./Textarea/Textarea";

class Main extends React.Component {
    render() {
        return (
            <main>
                <div className="body__main">
                    <div className="symbol-count">
                        Symbols: 0
                    </div>
                    <Textarea />
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