using ChapaTuRuta.Platform.API.Routes.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Routes.Domain.Model.Commands;

namespace ChapaTuRuta.Platform.API.Routes.Domain.Services;

public interface IStopRouteCommandService
{
    Task<StopRoute?> Handle(CreateStopRouteCommand command);
    
    Task Handle(DeleteStopRouteCommand command);
}