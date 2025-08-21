using AutoMapper;
using SchoolTransport.Application.Abstract;
using SchoolTransport.Application.DTOs.School;
using SchoolTransport.Application.DTOs.Student;
using SchoolTransport.Application.Exceptions.BadRequest;
using SchoolTransport.Application.Exceptions.NotFound;
using SchoolTransport.Application.Repositories;
using SchoolTransport.Domain.Entities.School;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace SchoolTransport.Application.Concrete
{
    public class SchoolService : ISchoolService
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public SchoolService(ISchoolRepository schoolRepository, IStudentRepository studentRepository, IMapper mapper)
        {
            _schoolRepository = schoolRepository;
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<List<SchoolResponse>> GetAllSchoolsAsync(string tenantId)
        {
            var schools = await _schoolRepository.GetAllAsync(tenantId);
            return _mapper.Map<List<SchoolResponse>>(schools);
        }

        public async Task<SchoolDetailResponse> GetSchoolDetailByIdAsync(int id, string tenantId)
        {
            var school = await _schoolRepository.GetByIdAsync(id, tenantId, tracking: false);
            if (school == null)
            {
                throw new SchoolNotFoundException($"ID'si {id} olan okul bulunamadı.");
            }

            var response = _mapper.Map<SchoolDetailResponse>(school);
            var students = await _schoolRepository.GetStudentsBySchoolIdAsync(id, tenantId);
            response.Students = _mapper.Map<List<StudentResponse>>(students);

            return response;
        }

        public async Task<SchoolResponse> CreateSchoolAsync(CreateSchoolRequest request, string tenantId)
        {
            if (request == null)
            {
                throw new SchoolBadRequestException("Okul oluşturma isteği boş olamaz.");
            }

            var school = await _schoolRepository.CreateSchoolAsync(request, tenantId);
            await _schoolRepository.SaveChangesAsync();
            return _mapper.Map<SchoolResponse>(school);
        }

        public async Task UpdateSchoolAsync(int id, UpdateSchoolRequest request, string tenantId)
        {
            if (request == null)
            {
                throw new SchoolBadRequestException("Okul güncelleme isteği boş olamaz.");
            }

            var school = await _schoolRepository.GetByIdAsync(id, tenantId, tracking: true);
            if (school == null)
            {
                throw new SchoolNotFoundException($"ID'si {id} olan okul bulunamadı.");
            }

            await _schoolRepository.UpdateSchoolWithFeeStructuresAsync(id, tenantId, request);
            await _schoolRepository.SaveChangesAsync();
        }

        public async Task DeleteSchoolAsync(int id, string tenantId)
        {
            var school = await _schoolRepository.GetByIdAsync(id, tenantId);
            if (school == null)
            {
                throw new SchoolNotFoundException($"ID'si {id} olan okul bulunamadı.");
            }

            await _schoolRepository.DeleteByIdAsync(id, tenantId);
            await _schoolRepository.SaveChangesAsync();
        }

        public async Task<List<StudentResponse>> GetStudentsBySchoolIdAsync(int schoolId, string tenantId)
        {
            var students = await _schoolRepository.GetStudentsBySchoolIdAsync(schoolId, tenantId);
            if (students == null || !students.Any())
            {
                throw new StudentNotFoundException($"ID'si {schoolId} olan okula kayıtlı öğrenci bulunamadı.");
            }
            return _mapper.Map<List<StudentResponse>>(students);
        }

        public async Task<List<SchoolResponse>> GetSchoolsByVehicleAsync(int vehicleId, string tenantId)
        {
            var schools = await _schoolRepository.GetSchoolsByVehicleAsync(vehicleId, tenantId);
            if (schools == null || !schools.Any())
            {
                throw new SchoolNotFoundException($"ID'si {vehicleId} olan araca kayıtlı okul bulunamadı.");
            }
            return _mapper.Map<List<SchoolResponse>>(schools);
        }

        public async Task<MonthlyPeriodsResponse> GetSchoolWithPeriodsAsync(int schoolId, string tenantId)
        {
            var school = await _schoolRepository.GetSchoolWithPeriodsAsync(schoolId, tenantId);

            if (schoolId <= 0)
            {
                throw new SchoolBadRequestException("Okul ID geçerli değil.");
            }

            if (school == null)
            {
                throw new SchoolNotFoundException($"ID'si {schoolId} olan okul bulunamadı.");
            }

            if (school.AcademicYearStartDate == default || school.AcademicYearEndDate == default || school.AcademicYearStartDate >= school.AcademicYearEndDate)
            {
                throw new SchoolBadRequestException("Bu okul için akademik yıl tarihleri tanımlanmamış veya geçersiz.");
            }

            var response = new MonthlyPeriodsResponse
            {
                SchoolName = school.Name,
                Periods = new List<PeriodInfo>()
            };

            var startDate = school.AcademicYearStartDate;
            var endDate = school.AcademicYearEndDate;
            var currentDate = new DateTime(startDate.Year, startDate.Month, 1);
            var installmentNumber = 1;

            while (currentDate <= endDate)
            {
                var period = new PeriodInfo
                {
                    Month = currentDate.Month,
                    Year = currentDate.Year,
                    MonthName = currentDate.ToString("MMMM", new System.Globalization.CultureInfo("tr-TR")),
                    PeriodDate = currentDate.ToString("yyyy-MM-dd"),
                    PeriodNumber = installmentNumber
                };

                response.Periods.Add(period);
                currentDate = currentDate.AddMonths(1);
                installmentNumber++;
            }

            return response;
        }

        public async Task<List<StudentResponse>> GetStudentsVehicleAndSchool(int vehicleId, int schoolId, string tenantId)
        {
            var students = await _schoolRepository.GetStudentsVehicleAndSchool(vehicleId, schoolId, tenantId);
            if (students == null || !students.Any())
            {
                throw new StudentNotFoundException($"ID'si {vehicleId} olan araçta, ID'si {schoolId} olan okula kayıtlı öğrenci bulunamadı.");
            }
            return _mapper.Map<List<StudentResponse>>(students);
        }

        public async Task<List<SchoolFeeStructureResponse>> GetSchoolFeeStructuresAsync(int schoolId, string tenantId)
        {
            if (schoolId <= 0)
            {
                throw new SchoolBadRequestException("Okul ID geçerli değil.");
            }

            var schoolFeeStructure = await _schoolRepository.GetSchoolFeeStructuresAsync(schoolId, tenantId);
            if (schoolFeeStructure == null || !schoolFeeStructure.Any())
            {
                throw new SchoolNotFoundException($"ID'si {schoolId} olan okula ait ücret yapısı bulunamadı.");
            }
            return _mapper.Map<List<SchoolFeeStructureResponse>>(schoolFeeStructure);
        }
    }
}