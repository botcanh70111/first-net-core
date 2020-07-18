import {Dispatch, AnyAction} from 'redux';
import { fetchBlog } from './blogServices';
import { Blog } from '../../constants/blogConstants';

export const getBlog = (slug: string) => {
    return (dispatch: Dispatch<AnyAction>) => {
        return fetchBlog(slug).then(function(r) {
            return dispatch({type: Blog.Detail, payload: r});
        });
    }
}