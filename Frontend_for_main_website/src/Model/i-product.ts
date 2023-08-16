import { ISubCategory } from './i-sub-category';
import { InformatinTableProducts } from "./informatin-table-products";

export interface IProduct {
  id: number;
  name: string;
  productNameEN: string;
  productNameAR: string;
  price: number;
  stock: number;
  quantity:number;
  image: string;
  brand: string;
  brandEN: string;
  brandAR: string;
  description: string;
  descriptionEN: string;
  descriptionAR: string;
  subcategoryId: number;
  subCategory: ISubCategory;
  discount?:number;
  rating:number;
  detailstable?: string[];
  informationTable?:InformatinTableProducts[];
  imagesdetails?: string[];
}
