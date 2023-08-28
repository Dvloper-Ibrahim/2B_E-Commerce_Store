using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentStatus { get; set; }
        public string TrackingInformation { get; set; }
        public string UserId { get; set; }
        public virtual ICollection<OrderItemDTO> OrderItems { get; set; }
        //public virtual ShippingDTO Shipping { get; set; }
        //public virtual PaymentDTO Payment { get; set; }
    }

}
