using EasyJet.KnightsTravel.Domain.Entities;
using EasyJet.KnightsTravel.Domain.Interfaces;
using EasyJet.KnightsTravel.Infrastructure.Services;

namespace EasyJet.KnightsTravel.Application.Services;

public class ChessMovementService : IChessMovementService
{
    private readonly IPieceMovement _pieceMovement;
    public ChessMovementService(IPieceMovement pieceMovement)
    {
        _pieceMovement = pieceMovement;
    }

    public List<Position> GetValidMoves(Board board, Position position)
    {
        var moves = _pieceMovement.GetPossibleMoves();
        var validMoves = new List<Position>();
        foreach (var move in moves)
        {
            var next = new Position(position.X + move.Dx, position.Y + move.Dy);
            if (board.IsInside(next) && !board.IsMarked(next))
            {
                validMoves.Add(next);
            }
        }

        return validMoves;
    }
}