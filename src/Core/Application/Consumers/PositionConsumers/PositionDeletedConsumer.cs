using Ardalis.Specification;
using Atos.Core.EventsDTO;
using Domain.Entities;
using Domain.Interfaces;
using MassTransit;

namespace Application.Consumers.PositionConsumers;

public class PositionDeletedConsumer : IConsumer<PositionDeleted>
{
    private readonly IClientPositionRepository _repository;

    public PositionDeletedConsumer(IClientPositionRepository repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<PositionDeleted> context)
    {
        var message = context.Message;
        var clientPositions = await _repository.ListAsync();

        foreach (var clientPosition in clientPositions.Where(s => s.PositionId == message.Id))
        {
            clientPosition.State = false;
            await _repository.UpdateAsync(clientPosition);
        }
    }
}