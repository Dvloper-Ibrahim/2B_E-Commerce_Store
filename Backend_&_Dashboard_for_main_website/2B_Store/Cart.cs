namespace _2B_Store
{
    public class Cart
    {

        public int Id { get; set; }
        public int Quantity { get; set; }


        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

    }
}