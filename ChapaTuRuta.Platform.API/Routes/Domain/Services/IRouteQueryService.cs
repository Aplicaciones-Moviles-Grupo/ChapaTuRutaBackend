using ChapaTuRuta.Platform.API.Routes.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Routes.Domain.Model.Queries;

namespace ChapaTuRuta.Platform.API.Routes.Domain.Services;

public interface IRouteQueryService
{
    Task<TravelRoute?> Handle(GetRouteByIdQuery query);
    
    Task<IEnumerable<TravelRoute>> Handle(GetRoutesByDriverIdQuery query);
}