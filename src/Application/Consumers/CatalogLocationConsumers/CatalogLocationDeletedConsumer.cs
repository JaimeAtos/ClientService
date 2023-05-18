using Application.Interfaces;
using Atos.Core.EventsDTO;
using Domain.Entities;
using MassTransit;

namespace Application.Consumers.CatalogLocationConsumers;

public class CatalogLocationDeletedConsumer : IConsumer<CatalogLocationDeleted>
{
    private readonly IRepositoryAsync<Client> _repository;

    public CatalogLocationDeletedConsumer(IRepositoryAsync<Client> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<CatalogLocationDeleted> context)
    {
        var message = context.Message;
        var clients = await _repository.ListAsync();

        foreach (var client in clients.Where(s => s.LocationId == message.LocationId))
        {
            client.State = false;
            await _repository.UpdateAsync(client);
        }
    }
}