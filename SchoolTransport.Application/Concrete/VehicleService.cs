using AutoMapper;
using SchoolTransport.Application.Abstract;
using SchoolTransport.Application.DTOs.School;
using SchoolTransport.Application.DTOs.Student;
using SchoolTransport.Application.DTOs.Vehicle;
using SchoolTransport.Application.Exceptions.BadRequest;
using SchoolTransport.Application.Exceptions.NotFound;
using SchoolTransport.Application.Repositories;
using SchoolTransport.Domain.Entities.Student;
using SchoolTransport.Domain.Entities.Vehicle;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Concrete
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly IMapper _mapper;

        public VehicleService(IVehicleRepository vehicleRepository, ISchoolRepository schoolRepository, IDriverRepository driverRepository, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _schoolRepository = schoolRepository;
            _driverRepository = driverRepository;
            _mapper = mapper;
        }

        public async Task<List<VehicleResponse>> GetAllVehiclesAsync(string tenantId)
        {
            var vehicles = await _vehicleRepository.GetAllAsync(tenantId, tracking: false);
            return _mapper.Map<List<VehicleResponse>>(vehicles);
        }

        public async Task<VehicleDetailResponse> GetVehicleDetailByIdAsync(int id, string tenantId)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id, tenantId, tracking: false);
            if (vehicle == null)
            {
                throw new VehicleNotFoundException($"ID'si {id} olan araç bulunamadı.");
            }
            return _mapper.Map<VehicleDetailResponse>(vehicle);
        }

        public async Task<VehicleResponse> CreateVehicleAsync(CreateVehicleRequest request, string tenantId)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.PlateNumber))
            {
                throw new VehicleBadRequestException("Araç oluşturma isteği geçersiz veya plaka numarası boş.");
            }

            var vehicle = _mapper.Map<Vehicle>(request);
            vehicle.TenantId = tenantId;
            var createdVehicle = await _vehicleRepository.AddAsync(vehicle);
            await _vehicleRepository.SaveChangesAsync();
            return _mapper.Map<VehicleResponse>(createdVehicle);
        }

        public async Task UpdateVehicleAsync(int id, UpdateVehicleRequest request, string tenantId)
        {
            if (request == null)
            {
                throw new VehicleBadRequestException("Araç güncelleme isteği boş olamaz.");
            }

            var vehicle = await _vehicleRepository.GetByIdAsync(id, tenantId);
            if (vehicle == null)
            {
                throw new VehicleNotFoundException($"ID'si {id} olan araç bulunamadı.");
            }

            _mapper.Map(request, vehicle);
            vehicle.TenantId = tenantId;
            await _vehicleRepository.UpdateAsync(vehicle);
            await _vehicleRepository.SaveChangesAsync();
        }

        public async Task DeleteVehicleAsync(int id, string tenantId)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id, tenantId);
            if (vehicle == null)
            {
                throw new VehicleNotFoundException($"ID'si {id} olan araç bulunamadı.");
            }
            await _vehicleRepository.DeleteByIdAsync(id, tenantId);
            await _vehicleRepository.SaveChangesAsync();
        }

        public async Task AssignSchoolToVehicleAsync(int vehicleId, int schoolId, string tenantId)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId, tenantId);
            if (vehicle == null)
            {
                throw new VehicleNotFoundException($"ID'si {vehicleId} olan araç bulunamadı.");
            }

            var school = await _schoolRepository.GetByIdAsync(schoolId, tenantId);
            if (school == null)
            {
                throw new SchoolNotFoundException($"ID'si {schoolId} olan okul bulunamadı.");
            }

            await _vehicleRepository.AssignSchoolToThePlate(vehicleId, schoolId, tenantId);
        }

        public async Task AssignDriverToVehicleAsync(int vehicleId, int driverId, string tenantId)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId, tenantId);
            if (vehicle == null)
            {
                throw new VehicleNotFoundException($"ID'si {vehicleId} olan araç bulunamadı.");
            }

            var driver = await _driverRepository.GetByIdAsync(driverId, tenantId);
            if (driver == null)
            {
                throw new DriverNotFoundException($"ID'si {driverId} olan sürücü bulunamadı.");
            }

            vehicle.DriverId = driverId;
            await _vehicleRepository.UpdateAsync(vehicle);
            await _vehicleRepository.SaveChangesAsync();
        }

        public async Task RemoveDriverFromVehicleAsync(int vehicleId, string tenantId)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId, tenantId);
            if (vehicle == null)
            {
                throw new VehicleNotFoundException($"ID'si {vehicleId} olan araç bulunamadı.");
            }

            vehicle.DriverId = null;
            await _vehicleRepository.UpdateAsync(vehicle);
            await _vehicleRepository.SaveChangesAsync();
        }

        public async Task<List<SchoolResponse>> GetSchoolsByVehicleIdAsync(int vehicleId, string tenantId)
        {
            var schools = await _vehicleRepository.GetSchoolsByVehicleIdAsync(vehicleId, tenantId);
            if (schools == null || schools.Count == 0)
            {
                throw new SchoolNotFoundException($"ID'si {vehicleId} olan araca atanmış okul bulunamadı.");
            }
            return _mapper.Map<List<SchoolResponse>>(schools);
        }

        public async Task<List<VehicleResponse>> GetVehicleBySchoolId(int schoolId, string tenantId)
        {
            var vehicles = await _vehicleRepository.GetVehicleBySchoolId(schoolId, tenantId);
            if (vehicles == null || vehicles.Count == 0)
            {
                throw new VehicleNotFoundException($"ID'si {schoolId} olan okula atanmış araç bulunamadı.");
            }
            List<VehicleResponse> vehicleResponse = _mapper.Map<List<VehicleResponse>>(vehicles);
            return vehicleResponse;
        }
    }
}