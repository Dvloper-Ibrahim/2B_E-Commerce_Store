using _2B_Store.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.Application11.Services
{
    public interface ILocationStoreServices
    {
        Task<List<LocationStoreDTO>> GetAllLocationStores();
        Task<LocationStoreDTO> GetLocationStoreById(int locationStoreId);
        Task<LocationStoreDTO> AddLocationStore(LocationStoreDTO locationStoreDTO);
        Task<LocationStoreDTO> UpdateLocationStore(int locationStoreId, LocationStoreDTO locationStoreDTO);
        Task DeleteLocationStore(int locationStoreId);
    }
}
