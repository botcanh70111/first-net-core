import axios from 'axios';
import { BlogModel } from '../blog/blogModel';
import { Hosts } from '../../constants/hosts';

export const fetchListBlogs = async () : Promise<BlogModel[]> => {
    const result = await axios.get<BlogModel[]>(Hosts.BaseUrl + `api/blog/list?bloggerId=${Hosts.BloggerId}`);

    return result.data;
}