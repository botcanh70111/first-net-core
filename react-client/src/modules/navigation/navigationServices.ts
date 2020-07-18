import axios from 'axios';
import { Hosts } from './../../constants/hosts';
import { Menu } from './navigationModel';

export const fetchNavigation = async () : Promise<Menu[]> => {
  const r = await axios.get<Menu[]>(Hosts.BaseUrl + `api/layout?bloggerId=${Hosts.BloggerId}`); 
  return r.data;
}