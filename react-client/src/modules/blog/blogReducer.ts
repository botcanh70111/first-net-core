import { BlogInfo } from "./blogModel";
import { Blog } from "../../constants/blogConstants";

const initState = {} as BlogInfo;
export const blogReducer = (state = initState, action: {type: string, payload: object}) : BlogInfo => {
    switch (action.type) {
        case Blog.Detail:
            return action.payload as BlogInfo;
        default:
            return state;
    }
}