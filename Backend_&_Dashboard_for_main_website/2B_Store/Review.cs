namespace _2B_Store
{
    public class Review
    {

        public int Id { get; set; }
        public int Rating { get; set; }
        public string Rev_Comment { get; set; }


        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

    }
}