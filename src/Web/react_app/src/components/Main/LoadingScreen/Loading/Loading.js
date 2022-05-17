import React, { useState, useRef } from 'react';
import { ThreeBounce } from 'better-react-spinkit'
import Timer from './Timer/Timer';
import Button from '@material-ui/core/Button';
import "./loading.css";

class Loading extends React.Component {
    render() {
        return (
            <div className='loading-container'>
                <Timer />
                <div className="loading">
                    <ThreeBounce size={30} color='#DFC777' />
                </div>
                <div className="cancel-button">
                    <Button
                        onClick={this.props.onClose}
                        style={{
                            fontFamily: 'Gardens',
                            backgroundColor: "#DFC777",
                            color: "black"
                        }}
                        color="primary"
                        variant="contained">Отменить
                    </Button>
                </div>
            </div>
        );
    }
}

export default Loading;