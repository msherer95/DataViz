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
module.hot.start();