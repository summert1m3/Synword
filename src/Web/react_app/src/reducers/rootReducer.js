const initialState = {
    text: "",
};

const rootReducer = (state = initialState, action) => {
    switch (action.type) {
        case 'CHANGE_TEXT':
            return {
                text: action.payload
            };
        default:
            return state;
    }
}

export default rootReducer;