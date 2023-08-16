using _2B_Store.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.Application11.Services
{
    public interface IShippingServices
    {
        Task<List<ShippingDTO>> GetAllShippings();

        Task<ShippingDTO> GetShippingById(int shippingId);
        Task<ShippingDTO> AddShipping(ShippingDTO shippingDTO);
        Task<ShippingDTO> UpdateShipping(int shippingId, ShippingDTO shippingDTO);
        Task DeleteShipping(int shippingId);
    }
}
