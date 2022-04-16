import React from "react";
import Header from "./header/Header";
import Main from "./main/Main";

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