using Ardalis.Specification;
using Atos.Core.EventsDTO;
using Domain.Entities;
using Domain.Interfaces;
using MassTransit;

namespace Application.Consumers.ResourceConsumers;

public class ResourceUpdatedConsumer : IConsumer<ResourceUpdated>
{
    private readonly IClientPositionManagerRepository _repository;

    public ResourceUpdatedConsumer(IClientPositionManagerRepository repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<ResourceUpdated> context)
    {
        var message = context.Message;
        var positionsManager = await _repository.ListAsync();

        foreach (var positionManager in positionsManager.Where(s => s.ResourceId == message.Id))
        {
            positionManager.Resource = message.ResourceName;
            await _repository.UpdateAsync(positionManager);
        }
    }
}