export interface Menu {
  id: string;
  name: string;
  urlLink: string;
  parentId: string;
  order: number;
  active: boolean;
  ownerId: string;
  childMenus: Array<Menu>;
}