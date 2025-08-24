using EasyJet.KnightsTravel.Domain.Interfaces;

namespace EasyJet.KnightsTravel.Domain.Entities;

public class KnightMovement : IPieceMovement
{
    public static Movement[] _knightMoves = new[]
    {
        new Movement(2,1),
        new Movement(2,-1),
        new Movement(-2,1),
        new Movement(-2,-1),
        new Movement(1,2),
        new Movement(1,-2),
        new Movement(-1,2),
        new Movement(-1,-2)
    };


    public Movement[] GetPossibleMoves()
    {
        return _knightMoves;
    }
}
