using ChapaTuRuta.Platform.API.Profiles.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Profiles.Domain.Model.Queries;

namespace ChapaTuRuta.Platform.API.Profiles.Domain.Services;

public interface IVehicleQueryService
{
    Task<Vehicle?> Handle(GetVehicleByIdQuery query);
    Task<Vehicle?> Handle(GetVehicleByProfileIdQuery query);
}