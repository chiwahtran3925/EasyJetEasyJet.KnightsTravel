using EasyJet.KnightsTravel.Domain.Entities;

namespace EasyJet.KnightsTravel.Domain.Interfaces;

public interface IPathFinderService
{
    public bool FindMatch(Board board);
}
