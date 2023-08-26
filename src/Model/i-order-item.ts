export interface IOrderItem {
  id: number;
  quantity: number;
  amount: number;
  paymentDate: Date;
  orderId: number;
  productId: number;
}
