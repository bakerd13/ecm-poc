import { applyMiddleware, combineReducers, compose, createStore } from 'redux';
import thunk from 'redux-thunk';
import searchReducer from "./reducers/searchReducer";
import filterReducer from "./reducers/filterReducer";
import shoppingBagReducer from "./reducers/shoppingBagReducer";
import menuReducer from "./reducers/menuReducer";

//import initSubscriber from 'redux-subscriber';

export default function configureStore () {  
  const reducers = {
    Search: searchReducer,
    Filter: filterReducer,
    ShoppingBag: shoppingBagReducer,
    Menu: menuReducer
  };

  const middleware = [
    thunk
  ];

  // In development, use the browser's Redux dev tools extension if installed
  const enhancers = [];
  const isDevelopment = process.env.NODE_ENV === 'development';
  if (isDevelopment && typeof window !== 'undefined' && window.devToolsExtension) {
    enhancers.push(window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__());
  }

  const rootReducer = combineReducers({
    ...reducers
  });

  const store = createStore(
    rootReducer,
    compose(applyMiddleware(...middleware), ...enhancers)
  );

  // TODO do we need subscriber as it can cause side effects on large components using it to update
  // "initSubscriber" returns "subscribe" function, so you can use it
  // const subscribe = initSubscriber(store);

  return store; 
}