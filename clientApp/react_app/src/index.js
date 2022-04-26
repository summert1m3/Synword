import React from "react";
import App from './components/App'
import { createRoot } from "react-dom/client";
import { Provider } from 'react-redux';
import { BrowserRouter as Router } from 'react-router-dom';
import "./components/app.css";
import "./components/Header/header.css";
import "./components/Main/main.css";
import "../node_modules/bootstrap/dist/css/bootstrap.min.css";
import "./components/Main/Textarea/textarea.css";
import "./components/Main/Header/symbolCount.css";
import "./components/Main/Footer/footerMain.css";
import "./components/Main/Loading/loading.css";
import "./components/Main/ResultsScreen/plagiarismCheckResultsScreen.css";
import "./components/Main/ResultsScreen/Link/link.css";
import "./components/Main/ResultsScreen/Header/header.css";
import "./components/Main/ResultsScreen/Percent/percent.css";
import "./components/Main/Loading/Timer/timer.css";

import store from "./store";

createRoot(document.getElementById("root")).render(
    <Provider store={store}>
        <Router>
            <App />
        </Router>
    </Provider>
);