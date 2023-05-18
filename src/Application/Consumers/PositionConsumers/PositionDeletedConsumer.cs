using Application.Interfaces;
using Atos.Core.EventsDTO;
using Domain.Entities;
using MassTransit;

namespace Application.Consumers.PositionConsumers;

public class PositionDeletedConsumer : IConsumer<PositionDeleted>
{
    private readonly IRepositoryAsync<ClientPosition> _repository;

    public PositionDeletedConsumer(IRepositoryAsync<ClientPosition> repository)
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