import React from "react";
import Main from "./Main/Main";
import { Route, Routes } from 'react-router-dom';
import "./app.css";

class App extends React.Component {
  render() {
    return (
      <Routes>
        <Route
          path="/"
          element={<Main/>}/>
      </Routes>
    );
  }
}

export default App;