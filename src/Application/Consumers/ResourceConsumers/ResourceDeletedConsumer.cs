using Application.Interfaces;
using Atos.Core.EventsDTO;
using Domain.Entities;
using MassTransit;

namespace Application.Consumers.ResourceConsumers;

public class ResourceDeletedConsumer : IConsumer<ResourceDeleted>
{
    private readonly IRepositoryAsync<ClientPositionManager> _managerRepository;
    private readonly IRepositoryAsync<LeaveRequest> _leaveRequestRepository;

    public ResourceDeletedConsumer(IRepositoryAsync<ClientPositionManager> managerRepository, IRepositoryAsync<LeaveRequest> leaveRequestRepository)
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