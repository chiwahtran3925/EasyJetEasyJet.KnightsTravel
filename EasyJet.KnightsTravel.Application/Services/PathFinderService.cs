using EasyJet.KnightsTravel.Domain.Entities;
using EasyJet.KnightsTravel.Domain.Interfaces;
using EasyJet.KnightsTravel.Infrastructure.Logger;

namespace EasyJet.KnightsTravel.Application.Services;

public class PathFinderService : IPathFinderService
{
    private readonly IChessMovementService _chessMovement;
    private readonly ILogger _logger;
    public PathFinderService(IChessMovementService chessMovement, ILogger logger)
    {
        _chessMovement = chessMovement;
        _logger = logger;
    }
    public bool FindMatch(Board board)
    {
        if(board.IsCompleted()) return true; 

        var validMoves = _chessMovement.GetValidMoves(board, board.GetPosition());

        validMoves = validMoves
        .OrderBy(move => _chessMovement.GetValidMoves(board, move).Count)
        .ToList();

        foreach (var move in validMoves) 
        {
            _logger.Log($"Marked ({move.X},{move.Y}) with move {board.NextStep()-1}");
            board.Mark(move, board.NextStep());
            if(FindMatch(board)) return true;

            _logger.Log($"UnMark ({move.X},{move.Y})");
            board.UnMark(move);
        }
        return false;
    }
}
