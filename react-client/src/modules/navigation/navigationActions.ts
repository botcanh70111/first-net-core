// import {fetchNavigation} from './navigationServices';
import { Dispatch, AnyAction} from 'redux';
import { fetchNavigation } from './navigationServices';

export const ActionTypes = {
  GetNav: "GetNav"
}

export const getNavigation = () => {
  return (dispatch: Dispatch<AnyAction>) => {
    return fetchNavigation()
    .then(function(r) {
      return dispatch({
        type: ActionTypes.GetNav,
        payload: r
      });
    });
  }
};