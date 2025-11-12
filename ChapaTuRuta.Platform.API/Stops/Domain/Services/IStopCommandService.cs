using ChapaTuRuta.Platform.API.Stops.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Stops.Domain.Model.Commands;

namespace ChapaTuRuta.Platform.API.Stops.Domain.Services;

public interface IStopCommandService
{
    Task<Stop?> Handle(CreateStopCommand command);
    Task<Stop?> Handle(UpdateStopCommand command);
    Task Handle(DeleteStopCommand command);
}