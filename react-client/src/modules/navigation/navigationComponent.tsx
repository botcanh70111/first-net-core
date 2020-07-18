import React, { useEffect } from 'react';
import { RootState } from '../reducers';
import { getNavigation } from './navigationActions';
import { bindActionCreators, AnyAction, Dispatch, ActionCreator } from 'redux';
import { connect } from 'react-redux';
import { Menu } from './navigationModel';
import { Link } from 'react-router-dom';

interface NavigationProps {
  menus: Menu[],
  getMenus: ActionCreator<any>
}

const NavigationComponent = (props: NavigationProps) => {

  useEffect(() => {
    props.getMenus();
  }, []);

  const renderMenu = (menu: Menu, isRoot = false) => {
    return menu !== undefined && <li key={menu.id}>
      <Link to={menu.urlLink ?? "/"}>
        {menu.name}
        {menu.childMenus.length > 0 && isRoot && <span className="menu-icon"><i className="fa fa-caret-right"></i></span>}
      </Link>
      {
        menu.childMenus.length > 0 &&
        <ul className="menu-child">
          {menu.childMenus.map((m) => renderMenu(m, true))}
        </ul>
      }
    </li>
  }

  return (
    <header>
      <div className="logo-center">
        <Link to="/">
          <img src="/logo192.png" alt="" />
        </Link>
      </div>

      <nav className="navigation-container">
        <div className="container full-height">
          <div className="row full-height">
            <div className="col-sm-12 navigation">
              <div className="header-menu">
                <ul className="menu title">
                  {props.menus.length > 0 && props.menus.map((m) => renderMenu(m))}
                </ul>
              </div>
              <div className="header-search">
                <input type="text" name="" id="" className="textbox" placeholder="Search..." />
                <div className="search-icon">
                  <i className="fa fa-search"></i>
                </div>
              </div>
            </div>
          </div>
        </div>
      </nav>
    </header>
  );
}

const mapStateToProps = (state: RootState) => {
  return {
    menus: state.menus
  }
}

const mapDispatchToProps = (dispatch: Dispatch<AnyAction>) => {
  return bindActionCreators({ getMenus: getNavigation }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(NavigationComponent);