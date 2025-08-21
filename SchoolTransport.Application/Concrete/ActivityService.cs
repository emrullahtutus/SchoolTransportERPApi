using AutoMapper;
using SchoolTransport.Application.Abstract;
using SchoolTransport.Application.DTOs.Activity;
using SchoolTransport.Application.Repositories;
using SchoolTransport.Domain.Entities.Activity;
using SchoolTransport.Domain.Entities.Driver;
using SchoolTransport.Domain.Entities.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Concrete
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;

        public ActivityService(IActivityRepository activityRepository, IMapper mapper)
        {
            _activityRepository = activityRepository;
            _mapper = mapper;
        }

        public async Task<ActivityResponseDto> CreateActivityAsync(CreateActivityRequest request, string tenantId)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var activity = _mapper.Map<Activity>(request);
            activity.TenantId = tenantId;
            await _activityRepository.AddAsync(activity);
            await _activityRepository.SaveChangesAsync();

            return _mapper.Map<ActivityResponseDto>(activity);

        }

        public async Task DeleteActivityAsync(int id, string tenantId)
        {
        await _activityRepository.DeleteByIdAsync(id, tenantId);
          await  _activityRepository.SaveChangesAsync();
        }

       
        public async Task<List<ActivityResponseDto>> GetAllActivityAsync(string tenantId)
        {
         var activity=   await _activityRepository.GetAllAsync(tenantId);
            return _mapper.Map <List<ActivityResponseDto>>(activity);
        }

        public async Task<ActivityResponseDto> GetByIdActivityAsync(int id,string tenantId)
        {
            var activity = await _activityRepository.GetByIdAsync(id, tenantId);
            return _mapper.Map<ActivityResponseDto>(activity);
        }

        public async Task UpdateActivityAsync(UpdateActivityRequest request, string tenantId)
        {
      var activity=await      _activityRepository.GetByIdAsync(request.Id, tenantId);
            _mapper.Map(request, activity);
            var activityUpdate= _mapper.Map<Activity>(activity);
            activityUpdate.TenantId = tenantId;
            
            await _activityRepository.UpdateAsync(activityUpdate);
            await _activityRepository.SaveChangesAsync();
        }

        public async Task<List<ActivityResponseDto>> VehicleByActivity(int vehicleId, string tenantId)
        {
          var avtivityList=await _activityRepository.VehicleByActivity(vehicleId, tenantId);
            return _mapper.Map<List<ActivityResponseDto>>(avtivityList);
        }



    }
}
