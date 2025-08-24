using EasyJet.KnightsTravel.Application.Exceptions;
using EasyJet.KnightsTravel.Application.Services;
using EasyJet.KnightsTravel.Application.Validators;
using EasyJet.KnightsTravel.Domain.Entities;
using EasyJet.KnightsTravel.Domain.Interfaces;
using EasyJet.KnightsTravel.Infrastructure.Logger;
using EasyJet.KnightsTravel.Infrastructure.Services;
using EasyJet.KnightsTravel.Presentation;


class Program
{
    static void Main()
    {
        var gameValidator = new GameValidator();
        var logger = new Logger();
        logger.Verbose = false; //Set to true for more logging

        var inputService = new InputService(gameValidator, logger);
        var pieceMovement = new KnightMovement();
        var chessMovement = new ChessMovementService(pieceMovement);
        var pathFinder = new PathFinderService(chessMovement, logger);

        var game = new GameLoop(inputService, pathFinder, logger, gameValidator);
        game.Run();
    }
}

public class GameLoop
{
    private readonly IPathFinderService _pathFinder;
    private readonly IInputService _inputService;
    private readonly ILogger _logger;
    private readonly IGameValidator _gameValidator;
    public GameLoop(IInputService inputService,  IPathFinderService pathFinder, ILogger logger, IGameValidator gameValidator)
    {
        _inputService = inputService;
        _pathFinder = pathFinder;
        _logger = logger;   
        _gameValidator = gameValidator;
    }

    public void Run()
    {
        var runAgain = true;

        while (runAgain)
        {
            _logger.Log("Game Start");
            try
            {
                var boardSize = _inputService.GetBoardSize();
                var position = _inputService.GetStartingPosition(boardSize);
                var board = new Board(boardSize, position);

                _logger.Info($"Board: {boardSize}X{boardSize}");
                _logger.Info($"Starting position: ({position.X},{position.Y})");
                var boardService = new BoardService(board);
                var game = new GameService(boardService, _pathFinder, _logger);
                if (game.PlayGame())
                {
                    _logger.Info("Full travel for Knight completed");
                    var resultboard = game.GetBoard();
                    var boardVisualiser = new BoardVisualiser(_logger, _gameValidator);
                    boardVisualiser.PrintBoard(resultboard);
                }
                else
                {
                    _logger.Info("No full travel for Knight");
                }
            }
            catch (Exception e) { _logger.Error(e.Message); }

            _logger.Log("Game ends");

            runAgain = _inputService.GetRunAgain();
            if (runAgain)
            {
                _logger.Info("Press any key to start a new game...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
