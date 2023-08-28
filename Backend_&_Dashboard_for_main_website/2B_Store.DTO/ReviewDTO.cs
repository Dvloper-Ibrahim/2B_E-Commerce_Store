using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.DTO
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Rev_Comment { get; set; }

        public int UserId { get; set; }
        public virtual UserDTO User { get; set; }

        public int ProductId { get; set; }
        public virtual ProductDTO Product { get; set; }

    }
}
