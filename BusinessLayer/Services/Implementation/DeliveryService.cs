using AutoMapper;
using BusinessLayer.Dto;
using BusinessLayer.Services.Interfaces;
using DataLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.Implementation
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeliveryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DeliveryDateDto>?> GetDateAsync(DateTime fromDateTime)
        {
            var result = await _unitOfWork.DeliveryDateTimeSlots.GetDeliveryDates(fromDateTime);
            return result != null ? _mapper.Map<IEnumerable<DeliveryDateDto>>(result) : null;
        }

        public async Task<IEnumerable<DeliveryTimeSlotDto>?> GetTimeSlotsAsync(DateTime date)
        {
            var result = await _unitOfWork.DeliveryDateTimeSlots.GetDeliveryTimeSlots(date);
            return result?.Select(x => new DeliveryTimeSlotDto { Id = x.Id, Time = x.TimeSlot.Time });
        }
    }
}
