import navigationReducer from "./navigation/navigationReducer";
import {combineReducers} from 'redux';
import { blogReducer } from "./blog/blogReducer";
import { listBlogsReducer } from "./listBlogs/listBlogsReducer";

const reducers = combineReducers({
    menus: navigationReducer, 
    blogDetail: blogReducer,
    listBlogs: listBlogsReducer
});

export type RootState = ReturnType<typeof reducers>;
export default reducers;