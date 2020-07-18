import { CategoryModel, TagModel } from "../category/categoryModel";

export interface BlogModel {
    id: string,
    name: string,
    description: string,
    content: string,
    blogUrl: string,
    categoryId: string,
    created: Date,
    createdBy: string,
    imageUrl: string,
    postScript: string,
    edited: Date,
    editedBy: string,
    views: number,
    bloggerId: string,
    blogTagIds: Array<string>,
}

export interface BlogInfo {
    blog: BlogModel,
    category: CategoryModel,
    tags: Array<TagModel>,
    author: UserInfo,
}

export interface UserInfo {
    id: string,
    phoneNumber: string,
    emailConfirmed: boolean,
    normalizedEmail: string,
    email: string,
    normalizedUserName: string,
    userName: string,
    firstName: string,
    lastName: string,
    avatar: string,
}