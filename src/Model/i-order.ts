import { IOrderItem } from './i-order-item';

export interface IOrder {
  id: number;
  orderDate: Date;
  arrivalDate: Date;
  totalAmount: number;
  paymentStatus: string;
  trackingInformation: string;
  userId: string;
  orderItems: IOrderItem[];
  // shippingId: number;
  // paymentId: number;
}
