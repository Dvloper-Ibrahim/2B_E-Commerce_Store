﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.Application.Contracts
{
    public interface IOrderItemRepository : IRepository<OrderItem, int>
    {
     //   Task<OrderItem> GetOrderItemById(int orderItemId);
    }
}
