// SchoolTransport.Application.Abstract/IDriverService.cs
using SchoolTransport.Application.DTOs.Driver;

namespace SchoolTransport.Application.Abstract
{
    public interface IDriverService
    {
        Task<List<DriverResponse>> GetAllDriversAsync(string tenantId);
        Task<DriverResponse> GetDriverByIdAsync(int id, string tenantId);
        Task<DriverResponse> CreateDriverAsync(CreateDriverRequest request, string tenantId);
        Task UpdateDriverAsync(int id, UpdateDriverRequest request, string tenantId);
        Task DeleteDriverAsync(int id, string tenantId);
    }
}