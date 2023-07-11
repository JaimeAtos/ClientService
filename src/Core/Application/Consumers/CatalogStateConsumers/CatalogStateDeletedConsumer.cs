using Ardalis.Specification;
using Atos.Core.EventsDTO;
using Domain.Entities;
using Domain.Interfaces;
using MassTransit;

namespace Application.Consumers.CatalogStateConsumers;

public class CatalogStateDeletedConsumer : IConsumer<CatalogStateDeleted>
{
    private readonly IClientPositionRepository _repository;

    public CatalogStateDeletedConsumer(IClientPositionRepository repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<CatalogStateDeleted> context)
    {
        var message = context.Message;
        var catalogState = await _repository.ListAsync();

        foreach (var states in catalogState.Where(s => s.CurrentStateId == message.Id))
        {
            states.State = false;
            await _repository.UpdateAsync(states);
        }
    }
}