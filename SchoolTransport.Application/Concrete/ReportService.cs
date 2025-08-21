using AutoMapper;
using SchoolTransport.Application.Abstract;
using SchoolTransport.Application.DTOs.School;
using SchoolTransport.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Concrete
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;

        public ReportService(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        public Task<int> GetAllSchoolCountAsync(string tenantId)
        {
            return _reportRepository.GetAllSchoolCountAsync(tenantId);
        }

        public Task<int> GetAllStudentCountAsync(string tenantId)
        {
            return _reportRepository.GetAllStudentCountAsync(tenantId);
        }

        public Task<int> GetAllVehicleCountAsync(string tenantId)
        {
            return _reportRepository.GetAllVehicleCountAsync(tenantId);
        }

        public async Task<int> GetSchoolAllStudentsCountAsync(int schoolId, string tenantId)
        {
            return await _reportRepository.GetSchoolAllStudentsCountAsync(schoolId, tenantId);
        }

        public Task<int> GetSchoolCountAsync(int vehicleId, string tenantId)
        {
            return _reportRepository.GetSchoolCountAsync(vehicleId, tenantId);
        }

        public Task<int> GetVehicleAllStudentCountAsync(int vehicleId, string tenantId)
        {
            return _reportRepository.GetVehicleAllStudentCountAsync(vehicleId, tenantId);
        }

        public Task<int> GetVehicleSchoolStudentCountAsync(int vehicleId, int schoolId, string tenantId)
        {
            return _reportRepository.GetVehicleSchoolStudentCountAsync(vehicleId, schoolId, tenantId);
        }

        public async Task<List<SchoolResponse>> GetVehivleSchoolList(int vehicleId, string tenantId)
        {
            var school = await _reportRepository.GetVehivleSchoolList(vehicleId, tenantId);
            return _mapper.Map<List<SchoolResponse>>(school);
        }
    }
}
