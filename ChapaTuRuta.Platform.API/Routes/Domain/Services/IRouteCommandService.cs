using ChapaTuRuta.Platform.API.Routes.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Routes.Domain.Model.Commands;

namespace ChapaTuRuta.Platform.API.Routes.Domain.Services;

public interface IRouteCommandService
{
    Task<TravelRoute?> Handle(CreateRouteCommand command);
    
    
    Task Handle(DeleteRouteCommand command);
    
    Task<TravelRoute?> Handle(ActiveRouteCommand command);
    
    Task<TravelRoute?> Handle(InactiveRouteCommand command);
}