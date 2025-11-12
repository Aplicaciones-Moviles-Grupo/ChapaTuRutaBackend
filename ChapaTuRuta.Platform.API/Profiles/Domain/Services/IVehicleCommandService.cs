using ChapaTuRuta.Platform.API.Profiles.Domain.Model.Commands;
using ChapaTuRuta.Platform.API.Profiles.Domain.Model.Aggregates;

namespace ChapaTuRuta.Platform.API.Profiles.Domain.Services;

public interface IVehicleCommandService
{
    Task<Vehicle?> Handle(CreateVehicleCommand command);
    Task<Vehicle?> Handle(UpdateVehicleCommand command);
}