import React from "react";
import Main from "./Main/Main";
import { Route, Routes } from 'react-router-dom';

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