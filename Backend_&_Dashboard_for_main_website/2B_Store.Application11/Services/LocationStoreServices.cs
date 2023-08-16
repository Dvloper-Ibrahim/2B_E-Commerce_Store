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
    public class LocationStoreServices
    {
        private readonly ILocationStoreRepository _locationStoreRepository;
        private readonly IMapper _mapper;

        public LocationStoreServices(ILocationStoreRepository locationStoreRepository, IMapper mapper)
        {
            _locationStoreRepository = locationStoreRepository;
            _mapper = mapper;
        }


        public async Task<List<LocationStoreDTO>> GetAllLocationStores()
        {
            var locationStores = await _locationStoreRepository.GetAllAsync();
            return _mapper.Map<List<LocationStoreDTO>>(locationStores);
        }

        public async Task<LocationStoreDTO> GetLocationStoreById(int locationStoreId)
        {
            var locationStore = await _locationStoreRepository.GetByIdAsync(locationStoreId);
            return _mapper.Map<LocationStoreDTO>(locationStore);
        }

        public async Task<LocationStoreDTO> AddLocationStore(LocationStoreDTO locationStoreDTO)
        {
            var locationStore = _mapper.Map<LocationStore>(locationStoreDTO);
            locationStore = await _locationStoreRepository.AddAsync(locationStore);
            await _locationStoreRepository.SaveChangesAsync();
            return _mapper.Map<LocationStoreDTO>(locationStore);
        }

        public async Task<LocationStoreDTO> UpdateLocationStore(int locationStoreId, LocationStoreDTO locationStoreDTO)
        {
            var existingLocationStore = await _locationStoreRepository.GetByIdAsync(locationStoreId);
            if (existingLocationStore == null)
                throw new ArgumentException("Location store not found");

            _mapper.Map(locationStoreDTO, existingLocationStore);
            existingLocationStore = await _locationStoreRepository.UpdateAsync(existingLocationStore);
            await _locationStoreRepository.SaveChangesAsync();
            return _mapper.Map<LocationStoreDTO>(existingLocationStore);
        }





        public async Task DeleteLocationStore(int locationStoreId)
        {
            var existingLocationStore = await _locationStoreRepository.GetByIdAsync(locationStoreId);
            if (existingLocationStore == null)
                throw new ArgumentException("Location store not found");

            await _locationStoreRepository.DeleteAsync(existingLocationStore);
            await _locationStoreRepository.SaveChangesAsync();
        }


    }
}
