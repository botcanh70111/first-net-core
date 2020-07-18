import { BlogModel } from "../blog/blogModel";
import { Blog } from "../../constants/blogConstants";

const initState = [] as BlogModel[];

export const listBlogsReducer = (state = initState, action: {type: string, payload: any}) : BlogModel[] => {
    switch(action.type) {
        case Blog.List:
            return action.payload as BlogModel[];

        default:
            return state;
    }
}