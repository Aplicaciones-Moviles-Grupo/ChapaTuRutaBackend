using ChapaTuRuta.Platform.API.Routes.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Routes.Domain.Model.Queries;
using ChapaTuRuta.Platform.API.Stops.Domain.Model.Queries;

namespace ChapaTuRuta.Platform.API.Routes.Domain.Services;

public interface IStopRouteQueryService
{
    Task<StopRoute?> Handle(GetStopRouteByIdQuery query);
    
    Task<IEnumerable<StopRoute>> Handle(GetStopRoutesByRouteIdQuery query);
}