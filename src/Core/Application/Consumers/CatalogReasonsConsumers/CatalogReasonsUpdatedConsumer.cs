using Atos.Core.EventsDTO;
using Domain.Interfaces;
using MassTransit;

namespace Application.Consumers.CatalogReasonsConsumers;

public class CatalogReasonsUpdatedConsumer : IConsumer<CatalogReasonsUpdated>
{
    private readonly ILeaveRequestRepository _repository;

    public CatalogReasonsUpdatedConsumer(ILeaveRequestRepository repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<CatalogReasonsUpdated> context)
    {
        var message = context.Message;
        var catalogReasons = await _repository.ListAsync();

        foreach (var reasons in catalogReasons.Where(s => s.ReasonId== message.Id))
        {
            reasons.LeaveReasonComments = message.Description;
            await _repository.UpdateAsync(reasons);
        }
    }
}