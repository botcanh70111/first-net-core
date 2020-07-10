import React from 'react';
import NavigationComponent from './modules/navigation/navigationComponent';
import { BrowserRouter, Route, Switch } from 'react-router-dom';

function App() {
  return (
    <BrowserRouter basename="/">
      <NavigationComponent />
      <div className="container regular-padding">
        <div className="row flex">
          <div className="col-sm-12">
            Hello
        </div>
        </div>
      </div>

      <div className="main-routes">
        <Switch>
          <Route path="/about" component={NavigationComponent}>
            <NavigationComponent></NavigationComponent>
          </Route>
        </Switch>
      </div>
    </BrowserRouter>
  );
}

export default App;
