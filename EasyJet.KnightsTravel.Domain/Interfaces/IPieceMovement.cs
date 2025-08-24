using EasyJet.KnightsTravel.Domain.Entities;

namespace EasyJet.KnightsTravel.Domain.Interfaces;

public interface IPieceMovement
{
    public Movement[] GetPossibleMoves();
}
