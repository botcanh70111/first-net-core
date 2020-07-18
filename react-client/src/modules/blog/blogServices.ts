import axios from 'axios';
import { Hosts } from '../../constants/hosts';
import { BlogModel } from './blogModel';

export const fetchBlog = async (slug: string) : Promise<BlogModel> => {
    const blog = await axios.get<BlogModel>(Hosts.BaseUrl + `api/blog?slug=${slug}&bloggerId=${Hosts.BloggerId}`);
    return blog.data;
}