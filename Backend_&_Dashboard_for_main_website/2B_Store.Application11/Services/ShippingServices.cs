using _2B_Store.Application.Contracts;
using _2B_Store.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.Application11.Services
{
    public class ShippingServices : IShippingServices
    {
        private readonly IShippingRepository _shippingRepository;
        private readonly IMapper _mapper;

        public ShippingServices(IShippingRepository shippingRepository, IMapper mapper)
        {
            _shippingRepository = shippingRepository;
            _mapper = mapper;
        }

        public async Task<List<ShippingDTO>> GetAllShippings()
        {
            var shippings = await _shippingRepository.GetAllAsync();
            return _mapper.Map<List<ShippingDTO>>(shippings);
        }

        public async Task<ShippingDTO> GetShippingById(int shippingId)
        {
            var shipping = await _shippingRepository.GetByIdAsync(shippingId);
            return _mapper.Map<ShippingDTO>(shipping);
        }

        public async Task<ShippingDTO> AddShipping(ShippingDTO shippingDTO)
        {
            var shipping = _mapper.Map<Shipping>(shippingDTO);
            shipping = await _shippingRepository.AddAsync(shipping);
            await _shippingRepository.SaveChangesAsync();
            return _mapper.Map<ShippingDTO>(shipping);
        }

        public async Task<ShippingDTO> UpdateShipping(int shippingId, ShippingDTO shippingDTO)
        {
            var existingShipping = await _shippingRepository.GetByIdAsync(shippingId);
            if (existingShipping == null)
                throw new ArgumentException("Shipping not found");

            _mapper.Map(shippingDTO, existingShipping);
            existingShipping = await _shippingRepository.UpdateAsync(existingShipping);
            await _shippingRepository.SaveChangesAsync();
            return _mapper.Map<ShippingDTO>(existingShipping);
        }

        public async Task DeleteShipping(int shippingId)
        {
            var existingShipping = await _shippingRepository.GetByIdAsync(shippingId);
            if (existingShipping == null)
                throw new ArgumentException("Shipping not found");

            await _shippingRepository.DeleteAsync(existingShipping);
            await _shippingRepository.SaveChangesAsync();
        }

    }
}
