using EasyJet.KnightsTravel.Application.Validators;
using EasyJet.KnightsTravel.Domain.Entities;
using EasyJet.KnightsTravel.Domain.Interfaces;
using EasyJet.KnightsTravel.Infrastructure.Logger;

namespace EasyJet.KnightsTravel.Presentation;

public  class BoardVisualiser : IBoardVisualiser
{
    private readonly ILogger _logger;
    private readonly IGameValidator _gameValidator;
    public BoardVisualiser(ILogger logger, IGameValidator gameValidator)
    {
        _logger = logger;
        _gameValidator = gameValidator;
    }
    public void PrintBoard(Board board)
    {
        var size = board.GetSize();
        _gameValidator.ValidateBoardSize(size);

        var cells = board.GetCells();

        for (var i = 0; i < size; i++)
        {
            for (var j = 0; j < size; j++)
            {
                _logger.Visual($"{cells[i,j]}\t");
            }
            Console.WriteLine();
        }
    }
}
