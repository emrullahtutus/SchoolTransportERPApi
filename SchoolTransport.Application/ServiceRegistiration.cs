using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SchoolTransport.Application.Abstract;
using SchoolTransport.Application.Concrete;
using SchoolTransport.Application.DTOs.Driver;
using SchoolTransport.Application.DTOs.Expenses;
using SchoolTransport.Application.DTOs.Payment;
using SchoolTransport.Application.DTOs.PaymentInstallment;
using SchoolTransport.Application.DTOs.PaymentTransaction;
using SchoolTransport.Application.DTOs.School;
using SchoolTransport.Application.DTOs.Student;
using SchoolTransport.Application.DTOs.User;
using SchoolTransport.Application.DTOs.Vehicle;
using SchoolTransport.Application.FluentValidation.Expenses;
using SchoolTransport.Application.FluentValidation.Payment;
using SchoolTransport.Application.FluentValidation.PaymentInstallment;

using SchoolTransport.Application.FluentValidation.Student;
using SchoolTransport.Application.Validators.Driver;
using SchoolTransport.Application.Validators.School;
using SchoolTransport.Application.Validators.User;
using SchoolTransport.Application.Validators.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application
{
    public static class ServiceRegistiration
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ISchoolService, SchoolService>();
            services.AddScoped<IDriverService, DriverService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IExpensesService, ExpensesService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IPaymentTransactionService, PaymentTransactionService>();
            services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());
            services.AddScoped<IActivityService, ActivityService>();

        }

        public static void AddValidators(this IServiceCollection services)
        {
            // Student Validators
            services.AddScoped<IValidator<CreateStudentRequest>, CreateStudentRequestValidator>();
            services.AddScoped<IValidator<UpdateStudentRequest>, UpdateStudentRequestValidator>();
            services.AddScoped<IValidator<CreateSchoolRequest>, CreateSchoolRequestValidator>();
            services.AddScoped<IValidator<UpdateSchoolRequest>, UpdateSchoolRequestValidator>();
            services.AddScoped<IValidator<CreateDriverRequest>, CreateDriverRequestValidator>();
            services.AddScoped<IValidator<UpdateDriverRequest>, UpdateDriverRequestValidator>();
            services.AddScoped<IValidator<CreateVehicleRequest>, CreateVehicleRequestValidator>();
            services.AddScoped<IValidator<UpdateVehicleRequest>, UpdateVehicleRequestValidator>();
            services.AddScoped<IValidator<CreatePaymentRequest>, CreatePaymentRequestValidator>();
            services.AddScoped<IValidator<UpdatePaymentRequest>, UpdatePaymentRequestValidator>();
            services.AddScoped<IValidator<CreatePaymentInstallmentRequest>, CreatePaymentInstallmentRequestValidator>();
            services.AddScoped<IValidator<UpdatePaymentInstallmentRequest>, UpdatePaymentInstallmentRequestValidator>();
            services.AddScoped<IValidator<ExpenseRequestDto>, ExpenseRequestDtoValidator>();
            services.AddScoped<IValidator<LoginRequest>, LoginRequestValidator>();

        }




    }
}

