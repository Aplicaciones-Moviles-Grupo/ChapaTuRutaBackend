using ChapaTuRuta.Platform.API.Routes.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Routes.Domain.Model.Queries;
using ChapaTuRuta.Platform.API.Routes.Domain.Repositories;
using ChapaTuRuta.Platform.API.Routes.Domain.Services;
using ChapaTuRuta.Platform.API.Shared.Domain.Repositories;

namespace ChapaTuRuta.Platform.API.Routes.Application.Internal.QueryServices;

public class StopRouteQueryService(IStopRouteRepository stopRouteRepository, IUnitOfWork unitOfWork):IStopRouteQueryService
{
    public async Task<StopRoute?> Handle(GetStopRouteByIdQuery query)
    {
        return await stopRouteRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<StopRoute>> Handle(GetStopRoutesByRouteIdQuery query)
    {
        return await stopRouteRepository.ListByRouteIdAsync(query.RouteId);
    }
}