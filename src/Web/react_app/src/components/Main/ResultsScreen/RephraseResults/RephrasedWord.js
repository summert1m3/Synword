import React from "react";
import "./rephraseResultsScreen.css";
import { useModal } from "react-simple-modal-provider";

const RephrasedWord = ({currentWord, synonyms, changeWord}) => {
    const { open } = useModal("PickSynonymsModal");

    return (
        <button onClick=
            {() => open({ 
                sourceWordSynonyms: {synonyms},
                changeWord:{changeWord} })} 
            className="open-dialog-button">
            <b>
                <p className="rephrasedWord">
                    {currentWord}
                </p>
            </b>
        </button>
    );
};

export default RephrasedWord;