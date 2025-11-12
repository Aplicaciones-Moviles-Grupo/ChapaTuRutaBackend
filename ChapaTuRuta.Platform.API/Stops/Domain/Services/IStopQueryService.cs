using ChapaTuRuta.Platform.API.Stops.Domain.Model.Aggregates;
using ChapaTuRuta.Platform.API.Stops.Domain.Model.Queries;

namespace ChapaTuRuta.Platform.API.Stops.Domain.Services;

public interface IStopQueryService
{
    Task<Stop?> Handle(GetStopByIdQuery query);
    Task<IEnumerable<Stop>> Handle(GetStopsByDriverIdQuery query);
}