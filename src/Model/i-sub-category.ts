import { IProduct } from './i-product';

export interface ISubCategory {
  id: number;
  name: string;
  nameEN: string;
  nameAR: string;
  categoryId: number;
  subCategoryId: number;
  subCategories: ISubCategory[];
  products: IProduct[];
}
