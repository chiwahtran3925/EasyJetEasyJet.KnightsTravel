using EasyJet.KnightsTravel.Application.Services;
using EasyJet.KnightsTravel.Application.Validators;
using EasyJet.KnightsTravel.Domain.Entities;
using EasyJet.KnightsTravel.Infrastructure.Logger;
using EasyJet.KnightsTravel.Infrastructure.Services;

namespace EasyJet.KnightsTravel.Application.Services.Helpers;

public static class GameServiceBuilder
{
    public static GameService Build(Board board)
    {
        var gameValidator = new GameValidator();
        var logger = new Logger();
        var pieceMovement = new KnightMovement();
        var chessMovement = new ChessMovementService(pieceMovement);
        var pathFinder = new PathFinderService(chessMovement, logger);

        var boardService = new BoardService(board);
        return new GameService(boardService, pathFinder, logger);
    }
}
