using ChapaTuRuta.Platform.API.Routes.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Routes.Domain.Model.Queries;
using ChapaTuRuta.Platform.API.Routes.Domain.Repositories;
using ChapaTuRuta.Platform.API.Routes.Domain.Services;
using ChapaTuRuta.Platform.API.Shared.Domain.Repositories;

namespace ChapaTuRuta.Platform.API.Routes.Application.Internal.QueryServices;

public class TravelRouteQueryService(IRouteRepository routeRepository, IUnitOfWork unitOfWork):IRouteQueryService
{
    public async Task<TravelRoute?> Handle(GetRouteByIdQuery query)
    {
        return await routeRepository.FindByIdAsync(query.Id);
    }
    
    public async Task<IEnumerable<TravelRoute>> Handle(GetRoutesByDriverIdQuery query)
    {
        return await routeRepository.ListByDriverIdAsync(query.DriverId);
    }
}