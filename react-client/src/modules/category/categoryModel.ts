export interface CategoryModel {
    id: string,
    name: string,
    description: string,
    content: string,
    categoryUrl: string,
    parentId: string,
    ownerId: string,
    childCategories: Array<CategoryModel>,
}

export interface TagModel {
    id: string,
    name: string,
    tagUrl: string,
    ownerId: string,
}