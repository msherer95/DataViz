import * as React from 'react';
import { Component } from 'react';
import * as ReactDom from 'react-dom';

class App extends Component {
    render() {
        return <h1>Some React!</h1>;
    }
}

ReactDom.render(<App/>, document.getElementById('app-container'));

// @ts-ignore
// Allows webpack to hot reload the entire component whenever it changes.
// Since this is the entry point of the app, this should reload on any change throughout the app.
module.hot.accept((() => {
    ReactDom.render(<App />, document.getElementById('app-container'));
}));