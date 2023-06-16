using Ardalis.Specification;
using Atos.Core.EventsDTO;
using Domain.Entities;
using Domain.Interfaces;
using MassTransit;

namespace Application.Consumers.ResourceConsumers;

public class ResourceDeletedConsumer : IConsumer<ResourceDeleted>
{
    private readonly IClientPositionManagerRepository _managerRepository;
    private readonly ILeaveRequestRepository _leaveRequestRepository;

    public ResourceDeletedConsumer(IClientPositionManagerRepository managerRepository, ILeaveRequestRepository leaveRequestRepository)
    {
        _managerRepository = managerRepository;
        _leaveRequestRepository = leaveRequestRepository;
    }

    public async Task Consume(ConsumeContext<ResourceDeleted> context)
    {
        var message = context.Message;
        var positionsManager = await _managerRepository.ListAsync();
        var leaveRequests = await _leaveRequestRepository.ListAsync();

        foreach (var positionManager in positionsManager.Where(s => s.ResourceId == message.Id))
        {
            positionManager.State = false;
            await _managerRepository.UpdateAsync(positionManager);
        }
        
        foreach (var leaveRequest in leaveRequests.Where(s => s.ResourceId == message.Id))
        {
            leaveRequest.State = false;
            await _leaveRequestRepository.UpdateAsync(leaveRequest);
        }

    }
}