using Application.Interfaces;
using Atos.Core.EventsDTO;
using Domain.Entities;
using MassTransit;

namespace Application.Consumers.CatalogStateConsumers;

public class CatalogStateUpdatedConsumer : IConsumer<CatalogStateUpdated>
{
    private readonly IRepositoryAsync<ClientPosition> _repository;

    public CatalogStateUpdatedConsumer(IRepositoryAsync<ClientPosition> repository)
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