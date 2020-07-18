import React from 'react';
import NavigationComponent from './modules/navigation/navigationComponent';
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import ListBlogsComponent from './modules/listBlogs/listBlogsComponent';
import BlogComponent from './modules/blog/blogComponent';

function App() {
  return (
    <BrowserRouter basename="/">
      <NavigationComponent />
      <div className="container regular-padding">
        <div className="row flex">
          <div className="col-sm-12">
            <Switch>
              <Route path="/about" component={NavigationComponent}>
                <NavigationComponent></NavigationComponent>
              </Route>
              <Route path="/blogs/:slug" component={BlogComponent}></Route>
              <Route path="/" component={ListBlogsComponent}></Route>
            </Switch>
          </div>
        </div>
      </div>
    </BrowserRouter>
  );
}

export default App;
