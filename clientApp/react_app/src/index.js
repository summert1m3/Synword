import React from "react";
import ReactDOM from "react-dom";
import App from './components/App'
import { createRoot } from "react-dom/client";
import "./styles/app.css"
import "./components/header/header.css"
import "./components/main/main.css"
import "../node_modules/bootstrap/dist/css/bootstrap.min.css"
import "./components/main/Textarea/textarea.css"
import "./components/main/Header/symbolCount.css"
import "./components/main/Footer/footerMain.css"
import "./components/main/Loading/loading.css"
import "./components/main/ResultsScreen/plagiarismCheckResultsScreen.css"
import "./components/main/ResultsScreen/Link/link.css"

createRoot(document.getElementById("root")).render(
    <App />
);