import React from "react";
import App from './components/App'
import { createRoot } from "react-dom/client";
import { Provider } from 'react-redux';
import { BrowserRouter as Router } from 'react-router-dom';
import "../node_modules/bootstrap/dist/css/bootstrap.min.css";
import { ModalProvider } from "react-simple-modal-provider";

import store from "./store";
import modals from "./components/Modals";

createRoot(document.getElementById("root")).render(
    <Provider store={store}>
        <Router>
            <ModalProvider value={modals}>
                <App />
            </ModalProvider>
        </Router>
    </Provider>
);