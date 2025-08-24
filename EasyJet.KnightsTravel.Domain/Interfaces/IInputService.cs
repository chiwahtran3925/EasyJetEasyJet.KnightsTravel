using EasyJet.KnightsTravel.Domain.Entities;

namespace EasyJet.KnightsTravel.Domain.Interfaces;

public interface IInputService
{
    public bool GetRunAgain();
    public int GetBoardSize();
    public Position GetStartingPosition(int boardSize);

}
