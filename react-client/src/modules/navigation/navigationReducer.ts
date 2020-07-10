import {ActionTypes} from './navigationActions';
import { Menu } from './navigationModel';

const initState = [] as Array<Menu>;

export const navigationReducer = (state: Array<Menu> = initState, action: {type: string, payload: object}) : Array<Menu> => {
  switch(action.type) {
    case ActionTypes.GetNav:
      return action.payload as Array<Menu>;
    default:
      return state;
  }
}

export default navigationReducer;