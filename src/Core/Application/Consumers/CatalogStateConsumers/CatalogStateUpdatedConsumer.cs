using Atos.Core.EventsDTO;
using Domain.Interfaces;
using MassTransit;

namespace Application.Consumers.CatalogStateConsumers;

public class CatalogStateUpdatedConsumer : IConsumer<CatalogStateUpdated>
{
    private readonly IClientPositionRepository _repository;

    public CatalogStateUpdatedConsumer(IClientPositionRepository repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<CatalogStateUpdated> context)
    {
        var message = context.Message;
        var catalogState = await _repository.ListAsync();

        foreach (var states in catalogState.Where(s => s.PositionId == message.Id))
        {
            states.CurrentStateName = message.Description;
            await _repository.UpdateAsync(states);
        }
    }
}