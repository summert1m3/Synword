
const changeText = (text) => {
    return {
        type: 'CHANGE_TEXT',
        payload: text
    };
};

export {
    changeText,
};