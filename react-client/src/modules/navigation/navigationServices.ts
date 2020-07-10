import axios from 'axios';
import { Hosts } from './../../constants/hosts';
import { Menu } from './navigationModel';

export const fetchNavigation = async () : Promise<Menu[]> => {
  const r = await axios.get<Menu[]>(Hosts.BaseUrl + "api/layout?bloggerId=0c03257e-06f7-430f-9e2c-1437f8d4bc16");
  return r.data;
}