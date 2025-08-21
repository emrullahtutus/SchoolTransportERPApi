using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolTransport.Application.Abstract;
using SchoolTransport.Application.DTOs.Driver;
using SchoolTransport.Application.Exceptions.BadRequest;
using SchoolTransport.Application.Exceptions.NotFound; // Bu satırı ekledim
using SchoolTransport.Application.Repositories;
using SchoolTransport.Domain.Entities.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Concrete
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IMapper _mapper;

        public DriverService(IDriverRepository driverRepository, IMapper mapper)
        {
            _driverRepository = driverRepository;
            _mapper = mapper;
        }

        public async Task<List<DriverResponse>> GetAllDriversAsync(string tenantId)
        {
            var drivers = await _driverRepository.GetAllAsync(tenantId);
            return _mapper.Map<List<DriverResponse>>(drivers);
        }

        public async Task<DriverResponse> GetDriverByIdAsync(int id, string tenantId)
        {
            var driver = await _driverRepository.GetByIdAsync(id, tenantId);
            if (driver == null)
            {
                throw new DriverNotFoundException($"ID'si {id} olan sürücü bulunamadı.");
            }
            return _mapper.Map<DriverResponse>(driver);
        }

        public async Task<DriverResponse> CreateDriverAsync(CreateDriverRequest request, string tenantId)
        {
            if (request == null)
            {
                throw new DriverBadRequestException("Sürücü oluşturma isteği boş olamaz.");
            }

            var driver = _mapper.Map<Driver>(request);
            driver.TenantId = tenantId;
            var addedDriver = await _driverRepository.AddAsync(driver);
            await _driverRepository.SaveChangesAsync();
            return _mapper.Map<DriverResponse>(addedDriver);
        }

        public async Task UpdateDriverAsync(int id, UpdateDriverRequest request, string tenantId)
        {
            if (request == null)
            {
                throw new DriverBadRequestException("Sürücü güncelleme isteği boş olamaz.");
            }

            var driver = await _driverRepository.GetByIdAsync(id, tenantId, tracking: true);
            if (driver == null)
            {
                throw new DriverNotFoundException($"ID'si {id} olan sürücü bulunamadı.");
            }

            _mapper.Map(request, driver);
            driver.TenantId = tenantId;
            await _driverRepository.UpdateAsync(driver);
            await _driverRepository.SaveChangesAsync();
        }

        public async Task DeleteDriverAsync(int id, string tenantId)
        {
            var driver = await _driverRepository.GetByIdAsync(id, tenantId);
            if (driver == null)
            {
                throw new DriverNotFoundException($"ID'si {id} olan sürücü bulunamadı.");
            }

            await _driverRepository.DeleteByIdAsync(id, tenantId);
            await _driverRepository.SaveChangesAsync();
        }
    }
}