using Application.Interfaces;
using Domain.Entities;
using MassTransit;

namespace Application.Consumers.CatalogReasonsConsumers;

public class CatalogReasonsDeletedConsumer : IConsumer<LeaveRequest>
{
    private readonly IRepositoryAsync<LeaveRequest> _repository;

    public CatalogReasonsDeletedConsumer(IRepositoryAsync<LeaveRequest> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<LeaveRequest> context)
    {
        var message = context.Message;
        var reasons = await _repository.ListAsync();

        foreach (var reason in reasons.Where(s => s.ClientPositionId == message.Id))
        {
            reason.State = false;
            await _repository.UpdateAsync(reason);
        }
    }
}