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

createRoot(document.getElementById("root")).render(
    <App />
);