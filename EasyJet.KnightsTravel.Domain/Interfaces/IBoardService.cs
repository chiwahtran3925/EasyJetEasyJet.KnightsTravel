using EasyJet.KnightsTravel.Domain.Entities;

namespace EasyJet.KnightsTravel.Domain.Interfaces;

public interface IBoardService
{
    public bool IsMarked(Position position);
    public int GetMoveCount();
    public Board GetBoard();
    public void Mark(Position position, int move);
    public void UnMark(Position position);

}
