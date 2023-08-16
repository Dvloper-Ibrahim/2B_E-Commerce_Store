using _2B_Store.Application.Contracts;
using _2B_Store.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.Infrastructure
{
    public class LocationStoreRepository : Repository<LocationStore , int> , ILocationStoreRepository
    {
        public LocationStoreRepository(StoreContext dbContext) : base(dbContext) { }
    }
}
