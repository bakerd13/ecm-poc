import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import { Provider } from 'react-redux';
import configureStore from './store/configureStore';
import * as AppConstants from "./config/constants";

const rootId = AppConstants.AppRoot;
const rootElement = document.getElementById(rootId);

// Get the application-wide store instance, prepopulating with state from the server where available.
//const initialState = window.initialReduxState;
const _store = configureStore();

ReactDOM.render(
    <React.StrictMode>
        <Provider store={_store}>
            <App />
        </Provider>,
    </React.StrictMode>,
    rootElement);

export { _store as store };
