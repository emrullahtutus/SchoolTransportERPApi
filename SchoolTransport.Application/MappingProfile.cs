using AutoMapper;
using SchoolTransport.Application.DTOs.Activity;
using SchoolTransport.Application.DTOs.Driver;
using SchoolTransport.Application.DTOs.Expenses;
using SchoolTransport.Application.DTOs.Payment;
using SchoolTransport.Application.DTOs.PaymentTransaction;
using SchoolTransport.Application.DTOs.School;
using SchoolTransport.Application.DTOs.Student;
using SchoolTransport.Application.DTOs.Vehicle;
using SchoolTransport.Domain.Entities.Activity;
using SchoolTransport.Domain.Entities.Driver;
using SchoolTransport.Domain.Entities.Expenses;
using SchoolTransport.Domain.Entities.Payment;
using SchoolTransport.Domain.Entities.School;
using SchoolTransport.Domain.Entities.Student;
using SchoolTransport.Domain.Entities.Vehicle;

namespace SchoolTransport.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Student Mappings
            CreateMap<Student, StudentResponse>();
            CreateMap<CreateStudentRequest, Student>();
            CreateMap<UpdateStudentRequest, Student>();
            CreateMap<Student, StudentFeeDto>().ReverseMap();

            // School Mappings
            CreateMap<School, SchoolResponse>();
            CreateMap<School, SchoolDetailResponse>();
            CreateMap<CreateSchoolRequest, School>()
                .ForMember(dest => dest.FeeStructures, opt => opt.MapFrom(src => src.FeeStructures));
            CreateMap<UpdateSchoolRequest, School>();
            CreateMap<SchoolFeeStructureDto, SchoolFeeStructure>();
            CreateMap<SchoolFeeStructure, SchoolFeeStructureResponse>();

            // Driver Mappings
            CreateMap<Driver, DriverResponse>();
            CreateMap<CreateDriverRequest, Driver>();
            CreateMap<UpdateDriverRequest, Driver>();

            // Vehicle Mappings
            CreateMap<Vehicle, VehicleResponse>();
            CreateMap<Vehicle, VehicleDetailResponse>();
            CreateMap<CreateVehicleRequest, Vehicle>();
            CreateMap<UpdateVehicleRequest, Vehicle>();

            // Payment Transaction Mappings
            CreateMap<PaymentTransaction, PaymentTransactionResponse>();
            CreateMap<PaymentTransactionResponse, PaymentTransaction>();

            // Expense Mappings
            CreateMap<ExpenseRequestDto, Expense>();
            CreateMap<Expense, ExpenseResponseDto>();
            CreateMap<ExpenseRequestDto, Expense>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<CreateActivityRequest, Activity>();
            CreateMap<UpdateActivityRequest, Activity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.TenantId, opt => opt.Ignore());
            CreateMap<Activity, ActivityResponseDto>();
        }
    }
}