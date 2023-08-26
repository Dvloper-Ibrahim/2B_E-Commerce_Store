import { IProduct } from './i-product';

export interface ILocationStore {
  id: number;
  nameEN: string;
  nameAR: string;

  streetEN: string;
  streetAR: string;

  cityEN: string;
  cityAR: string;

  countryEN: string;
  countryAR: string;

  imageStore: string;
  tel_Number: string;

  products: IProduct[];
}
