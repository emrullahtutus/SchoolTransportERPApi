using AutoMapper;
using SchoolTransport.Application.Abstract;
using SchoolTransport.Application.DTOs.School;
using SchoolTransport.Application.DTOs.Student;
using SchoolTransport.Application.DTOs.Vehicle;
using SchoolTransport.Application.Exceptions.BadRequest;
using SchoolTransport.Application.Exceptions.NotFound;
using SchoolTransport.Application.Repositories;
using SchoolTransport.Domain.Entities.Payment;
using SchoolTransport.Domain.Entities.School;
using SchoolTransport.Domain.Entities.Student;
using SchoolTransport.Domain.Entities.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Concrete
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPaymentTransactionRepository _paymentTransactionRepository;
        private readonly ISchoolService _schoolService;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository studentRepository, ISchoolRepository schoolRepository, IMapper mapper, IVehicleRepository vehicleRepository, IPaymentRepository paymentRepository, ISchoolService schoolService, IPaymentTransactionRepository paymentTransactionRepository)
        {
            _studentRepository = studentRepository;
            _schoolRepository = schoolRepository;
            _mapper = mapper;
            _vehicleRepository = vehicleRepository;
            _paymentRepository = paymentRepository;
            _schoolService = schoolService;
            _paymentTransactionRepository = paymentTransactionRepository;
        }

        public async Task<List<StudentResponse>> GetAllAsync(string tenantId)
        {
            var students = await _studentRepository.GetAllAsync(tenantId);
            return _mapper.Map<List<StudentResponse>>(students);
        }

        public async Task<StudentResponse> GetByIdAsync(int id, string tenantId)
        {
            var student = await _studentRepository.GetByIdAsync(id, tenantId);
            if (student == null)
            {
                throw new StudentNotFoundException($"ID'si {id} olan öğrenci bulunamadı.");
            }
            return _mapper.Map<StudentResponse>(student);
        }

        public async Task DeleteAsync(int id, string tenantId)
        {
            var student = await _studentRepository.GetByIdAsync(id, tenantId);
            if (student == null)
            {
                throw new StudentNotFoundException($"ID'si {id} olan öğrenci bulunamadı.");
            }
            await _studentRepository.DeleteByIdAsync(id, tenantId);
            await _studentRepository.SaveChangesAsync();
        }

        public async Task AssignSchoolToStudentAsync(int studentId, int schoolId, string tenantId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId, tenantId);
            if (student == null)
            {
                throw new StudentNotFoundException($"ID'si {studentId} olan öğrenci bulunamadı.");
            }

            var school = await _schoolRepository.GetByIdAsync(schoolId, tenantId);
            if (school == null)
            {
                throw new SchoolNotFoundException($"ID'si {schoolId} olan okul bulunamadı.");
            }

            student.SchoolId = schoolId;
            await _studentRepository.UpdateAsync(student);
            await _studentRepository.SaveChangesAsync();
        }

        public async Task AssignVehicleToStudentAsync(int studentId, int? vehicleId, string tenantId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId, tenantId);
            if (student == null)
            {
                throw new StudentNotFoundException($"ID'si {studentId} olan öğrenci bulunamadı.");
            }

            if (vehicleId != null)
            {
                var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId.Value, tenantId);
                if (vehicle == null)
                {
                    throw new VehicleNotFoundException($"ID'si {vehicleId} olan araç bulunamadı.");
                }
            }

            student.VehicleId = vehicleId;
            await _studentRepository.UpdateAsync(student);
            await _studentRepository.SaveChangesAsync();
        }

        public async Task<VehicleResponse> GetStudentsAssignedToVehicle(int studentId, string tenantId)
        {
            var vehicle = await _studentRepository.GetStudentsAssignedToVehicle(studentId, tenantId);
            if (vehicle == null)
            {
                throw new VehicleNotFoundException($"ID'si {studentId} olan öğrenciye atanmış bir araç bulunamadı.");
            }
            return _mapper.Map<VehicleResponse>(vehicle);
        }

        public async Task<StudentResponse> CreateAsync(CreateStudentRequest request, string tenantId)
        {
            if (request == null)
            {
                throw new StudentBadRequestException("Öğrenci oluşturma isteği boş olamaz.");
            }

            decimal total = 0;
            var student = _mapper.Map<Domain.Entities.Student.Student>(request);

            student.TenantId = tenantId;

            // Okul ve ödeme dönemi kontrolleri
            var school = await _schoolRepository.GetByIdAsync(request.SchoolId, tenantId);
            if (school == null)
            {
                throw new SchoolNotFoundException($"ID'si {request.SchoolId} olan okul bulunamadı.");
            }

            student.MonthlyFee = await _studentRepository.GetMonthlyFeeByDistanceAsync(request.SchoolId, request.Distance, tenantId);
            var periods = await _schoolService.GetSchoolWithPeriodsAsync(request.SchoolId, tenantId);
            int countMonth = periods.Periods.Count;

            if (student.MonthlyFee != null)
            {
                total = countMonth * student.MonthlyFee.Value;
            }

            var createdStudent = await _studentRepository.AddAsync(student);
            await _studentRepository.SaveChangesAsync();

            var payment = new Payment()
            {
                StudentId = createdStudent.Id,
                MonthlyFee = (student.MonthlyFee ?? 0m),
                TotalFee = total,
                PaidAmount = 0,
                RemainingAmount = total,
                TenantId = tenantId
            };

            await _paymentRepository.AddAsync(payment);
            await _paymentRepository.SaveChangesAsync();

            return _mapper.Map<StudentResponse>(createdStudent);
        }

        public async Task<StudentResponse> UpdateAsync(int id, UpdateStudentRequest request, string tenantId)
        {
            if (request == null)
            {
                throw new StudentBadRequestException("Öğrenci güncelleme isteği boş olamaz.");
            }

            var student = await _studentRepository.GetByIdAsync(id, tenantId, tracking: true);
            if (student == null)
            {
                throw new StudentNotFoundException($"ID'si {id} olan öğrenci bulunamadı.");
            }

            var transactions = await _paymentTransactionRepository.GetTransactionByIdAsync(id, tenantId);
            decimal transactionTotalPaidAmount = transactions.Sum(t => t.Amount);

            if (student.Distance != request.Distance)
            {
                student.MonthlyFee = await _studentRepository.GetMonthlyFeeByDistanceAsync(request.SchoolId, request.Distance, tenantId);

                var periods = await _schoolService.GetSchoolWithPeriodsAsync(request.SchoolId, tenantId);
                int countMonth = periods.Periods.Count;

                decimal total = (student.MonthlyFee ?? 0m) * countMonth;

                var payment = await _paymentRepository.GetByIdAsync(student.Id, tenantId);
                if (payment == null)
                {
                    payment = new Payment { StudentId = student.Id, TenantId = tenantId };
                    await _paymentRepository.AddAsync(payment);
                }

                payment.MonthlyFee = student.MonthlyFee ?? 0m;
                payment.TotalFee = total;
                payment.PaidAmount = transactionTotalPaidAmount;
                payment.RemainingAmount = total - transactionTotalPaidAmount;
                payment.TenantId = tenantId;

                await _paymentRepository.UpdateAsync(payment);
            }

            _mapper.Map(request, student);
            student.TenantId = tenantId;
            await _studentRepository.UpdateAsync(student);
            await _studentRepository.SaveChangesAsync();

            return _mapper.Map<StudentResponse>(student);
        }

        public async Task<List<StudentFeeDto>> GetStudentFeeAsync(int schoolId, int vehicleId, string tenantId)
        {
            var students = await _studentRepository.GetStudentFee(schoolId, vehicleId, tenantId);

            if (students == null || !students.Any())
            {
                throw new StudentNotFoundException($"ID'si {schoolId} olan okulda, ID'si {vehicleId} olan araca kayıtlı öğrenci bulunamadı.");
            }

            var studentFeeDtos = students.Select(student => new StudentFeeDto
            {
                Id = student.Id,
                FullName = student.FullName,
                MonthlyFee = student.MonthlyFee,
                PhoneNumber = student.PhoneNumber
            }).ToList();

            return studentFeeDtos;
        }
    }
}