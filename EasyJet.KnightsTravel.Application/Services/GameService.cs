using EasyJet.KnightsTravel.Domain.Entities;
using EasyJet.KnightsTravel.Domain.Interfaces;
using EasyJet.KnightsTravel.Infrastructure.Logger;

namespace EasyJet.KnightsTravel.Application.Services;

public class GameService : IGameService
{
    private readonly IBoardService _boardService;
    private readonly IPathFinderService _pathFinder;
    private readonly ILogger _logger;
    private Board _board;
    public GameService(IBoardService boardService, IPathFinderService pathFinder, ILogger logger)
    {
        _boardService = boardService;
        _pathFinder = pathFinder;
        _board = _boardService.GetBoard();
        _logger = logger;
    }

    public bool PlayGame()
    {
        _logger.Log("Starting game...");
        var result = _pathFinder.FindMatch(_board);
        _logger.Log(result ? "Full path complete" : "No full path completed");
        return result;
    }
    public Board GetBoard() 
    {
        return _board;
    }

}
