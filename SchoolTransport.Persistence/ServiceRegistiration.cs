using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolTransport.Application.Repositories;
using SchoolTransport.Persistence.Context;
using SchoolTransport.Persistence.Repositories;


namespace SchoolTransport.Persistence
{
    public static class ServiceRegistiration
    {
        public static void ConfigurePersistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SchoolTransportDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ISchoolRepository, SchoolRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IDriverRepository, DriverRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentTransactionRepository, PaymentTransactionRepository>();
            services.AddScoped<IExpensesRepository, ExpensesRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();  
            services.AddScoped<IBulkOperationRepository, BulkOperationRepository>();
            services.AddScoped<IActivityRepository, ActivityRepository>();
           
        }
    }
}