import navigationReducer from "./navigation/navigationReducer";
import {combineReducers} from 'redux';

const reducers = combineReducers({menus: navigationReducer});
export type RootState = ReturnType<typeof reducers>;
export default reducers;