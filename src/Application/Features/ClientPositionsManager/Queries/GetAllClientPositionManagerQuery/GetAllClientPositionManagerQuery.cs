﻿using Application.DTOs;
using Application.Interfaces;
using Application.Specification;
using Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.ClientPositionsManager.Queries.GetAllClientPositionManagerQuery
{
    public class GetAllClientPositionManagerQuery : IRequest<PagedResponse<List<ClientPositionManagerDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Resource { get; set; }
        public bool State { get; set; }
    }

    public class GetAllClientPositionManagerHandler : IRequestHandler<GetAllClientPositionManagerQuery, PagedResponse<List<ClientPositionManagerDTO>>>
    {
        private readonly IRepositoryAsync<Domain.Entities.ClientPositionManager> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetAllClientPositionManagerHandler(IRepositoryAsync<Domain.Entities.ClientPositionManager> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public Task<PagedResponse<List<ClientPositionManagerDTO>>> Handle(GetAllClientPositionManagerQuery request, CancellationToken cancellationToken)
        {
            
            if (request == null) throw new ArgumentNullException(nameof(request));
            return HandleProcess(request, cancellationToken);
        }

        private async Task<PagedResponse<List<ClientPositionManagerDTO>>> HandleProcess(GetAllClientPositionManagerQuery request, CancellationToken cancellationToken)
        {
            var pagination = new PagedClientPositionManagerSpecification(request);
            var clientPositionManager = await _repositoryAsync.ListAsync(pagination, cancellationToken);
            var clientPositionManagerDto = _mapper.Map<List<ClientPositionManagerDTO>>(clientPositionManager);
            return new PagedResponse<List<ClientPositionManagerDTO>>(clientPositionManagerDto, request.PageNumber, request.PageSize);
        }
    }



}
