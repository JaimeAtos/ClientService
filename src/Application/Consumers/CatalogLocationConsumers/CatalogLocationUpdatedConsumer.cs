using Application.Interfaces;
using Atos.Core.EventsDTO;
using Domain.Entities;
using MassTransit;

namespace Application.Consumers.CatalogLocationConsumers;

public class CatalogLocationUpdatedConsumer : IConsumer<CatalogLocationUpdated>
{
    private readonly IRepositoryAsync<Client> _repository;

    public CatalogLocationUpdatedConsumer(IRepositoryAsync<Client> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<CatalogLocationUpdated> context)
    {
        var message = context.Message;
        var clients = await _repository.ListAsync();

        foreach (var client in clients.Where(s => s.LocationId == message.LocationId))
        {
            client.LocationName = $"{message.CityDescription} - {message.CountryDescription}";
            await _repository.UpdateAsync(client);
        }
    }
}