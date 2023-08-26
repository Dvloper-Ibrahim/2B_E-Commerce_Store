import { ISubCategory } from './i-sub-category';

export interface ICategory {
  id: number;
  name:string;
  nameEN: string;
  nameAR: string;
  type: string;
  image: string;
  description: string;
  descriptionEN: string;
  descriptionAR: string;
  subCategories: ISubCategory[];
}
