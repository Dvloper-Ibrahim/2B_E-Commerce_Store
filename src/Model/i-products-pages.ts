import { InformatinTableProducts } from "./informatin-table-products";

export interface IProductsPages {
    id:number;
    name:string;
    quantity:number;
    price:number;
    imgURL:string;
    imagesdetails: string[];
    categoryId:number;
    categoryName:string;
    subCategoryName?:string;
    subCategoryId?:number;
    subSubCategoryName?:string;
    subSubCategoryId:number;
    details: string;
    material?:string;
    brand?:string;
    rating?:number;
    description?:string;
    discount?:number;
    screenSize?:any;
    screenResolution?:any;
    warranty?:any;
    title?: string;
    detailstable?: string[];
    informationTable?:InformatinTableProducts[];
}
