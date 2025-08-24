using EasyJet.KnightsTravel.Domain.Entities;

namespace EasyJet.KnightsTravel.Domain.Interfaces;

public interface IGameService
{
    public bool PlayGame();
    public Board GetBoard();

}
