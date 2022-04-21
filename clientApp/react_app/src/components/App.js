import React from "react";
import Header from "./Header/Header";
import Main from "./Main/Main";

class App extends React.Component {
  render() {
    return (
      <div>
        <Header />
        <main>
          <Main />
        </main>
      </div>
    );
  }
}

export default App;