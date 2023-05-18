using Application.Interfaces;
using Atos.Core.EventsDTO;
using Domain.Entities;
using MassTransit;

namespace Application.Consumers.CatalogStateConsumers;

public class CatalogStateDeletedConsumer : IConsumer<CatalogStateDeleted>
{
    private readonly IRepositoryAsync<ClientPosition> _repository;

    public CatalogStateDeletedConsumer(IRepositoryAsync<ClientPosition> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<CatalogStateDeleted> context)
    {
        var message = context.Message;
        var catalogState = await _repository.ListAsync();

        foreach (var states in catalogState.Where(s => s.PositionId == message.Id))
        {
            states.State = false;
            await _repository.UpdateAsync(states);
        }
    }
}