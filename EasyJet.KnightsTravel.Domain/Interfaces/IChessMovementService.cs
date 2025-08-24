using EasyJet.KnightsTravel.Domain.Entities;

namespace EasyJet.KnightsTravel.Domain.Interfaces;

public interface IChessMovementService
{
    public List<Position> GetValidMoves(Board board, Position position);
}
