import React from 'react'
import { useState, useEffect } from 'react';

const Timer = () => {
    const [minutes, setMinutes] = useState(0);
    const [seconds, setSeconds] = useState(0);
    useEffect(() => {
        let myInterval = setInterval(() => {
            if (seconds < 59) {
                setSeconds(seconds + 1);
            }
            else {
                setMinutes(minutes + 1);
                setSeconds(0);
            }
        }, 1000)
        return () => {
            clearInterval(myInterval);
        };
    });

    return (
        <div className="timer">
            <p> {minutes}:{seconds < 10 ? `0${seconds}` : seconds}</p>
        </div>
    )
}

export default Timer;