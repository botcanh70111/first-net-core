import { fetchListBlogs } from "./listBlogsServices"
import { Dispatch, AnyAction } from "redux";
import { Blog } from "../../constants/blogConstants";

export const getListBlogs = () => {
    return (dispatch: Dispatch<AnyAction>) => {
        return fetchListBlogs().then(function(r) {
            return dispatch({type: Blog.List, payload: r});
        })
    }
}