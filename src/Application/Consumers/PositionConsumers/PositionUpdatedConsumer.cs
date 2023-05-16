using Application.Interfaces;
using Atos.Core.EventsDTO;
using Domain.Entities;
using MassTransit;

namespace Application.Consumers.PositionConsumers;

public class PositionUpdatedConsumer : IConsumer<PositionUpdated>
{
    private readonly IRepositoryAsync<ClientPosition> _repository;

    public PositionUpdatedConsumer(IRepositoryAsync<ClientPosition> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<PositionUpdated> context)
    {
        var message = context.Message;
        var clientPositions = await _repository.ListAsync();

        foreach (var clientPosition in clientPositions.Where(s => s.PositionId == message.Id))
        {
            clientPosition.PositionDescription = message.Description;
            await _repository.UpdateAsync(clientPosition);
        }
    }
}