import { useModalProps } from "react-simple-modal-provider";
import "./pickSynonymsModalBody.css";

const PickSynonymsModalBody = ({ onCloseHandler }) => {
    const { sourceWordSynonyms, changeWord } =
        useModalProps("PickSynonymsModal");

    const {
        sourceWord,
        synonyms
    } = sourceWordSynonyms.synonyms;

    let synonymsArr = [];

    synonyms.forEach(function (item, index, array) {
        const {
            value
        } = item;
        synonymsArr.push(
            <p className="words">
                <button
                    onClick={() => {
                        changeWord.changeWord(value);
                        onCloseHandler();
                    }}
                    className="pick-word-button">{value}</button>
            </p>);
    });

    return (
        <div className="wrapper">
            <p className="title">Замена слов</p>
            <p className="subtitle">Исходное</p>

            <p className="words">
                <button
                    onClick={() => {
                        changeWord.changeWord(sourceWord);
                        onCloseHandler();
                    }}
                    className="pick-word-button">{sourceWord}
                </button>
            </p>

            <hr className="border" />
            <p className="subtitle">Синонимы</p>
            {synonymsArr}
        </div>
    );
};

export default PickSynonymsModalBody;
