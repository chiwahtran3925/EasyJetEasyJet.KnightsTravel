using EasyJet.KnightsTravel.Application.Exceptions;
using EasyJet.KnightsTravel.Domain.Entities;
using EasyJet.KnightsTravel.Domain.Interfaces;
using EasyJet.KnightsTravel.Infrastructure.Logger;

namespace EasyJet.KnightsTravel.Presentation;

public class InputService : IInputService
{
    private readonly IGameValidator _gameValidator;
    private readonly ILogger _logger;
    public InputService(IGameValidator gameValidator, ILogger logger)
    {
        _gameValidator = gameValidator;
        _logger = logger;
    }

    public bool GetRunAgain()
    {
        _logger.Info("Do you want to play again? (Yes/No):");

        var runInput = Console.ReadLine()?.Trim().ToLower();

        while (runInput != "yes" && runInput != "no" && runInput != "y" && runInput != "n")
        {
            _logger.Info("Invalid input. Please enter Yes/No:");
            runInput = Console.ReadLine();
        }

        return (runInput == "yes" || runInput == "y");

    }

    public int GetBoardSize()
    {
        int boardSize;
        while (true)
        {
            _logger.Info("Enter board size between 5-8:");
            if (int.TryParse(Console.ReadLine(), out boardSize) && boardSize > 0)
            {
                try
                {
                    _gameValidator.ValidateBoardSize(boardSize);
                    break;
                }
                catch (InvalidBoardSizeException ex)
                {
                    _logger.Error(ex.Message);
                    _logger.Log($"Incorrect board size {boardSize}");

                }
            }
            else
            {
                _logger.Info("Invalid board size. Please enter a valid number.");
            }
        }
        return boardSize;
    }

  
    public Position GetStartingPosition( int boardSize)
    {
        int positionX = ReadPosition("X", boardSize);
        int positionY = ReadPosition("Y", boardSize);
        return new Position(positionX, positionY);
    }

    private int ReadPosition(string axis, int boardSize)
    {
        int positionValue;

        while (true)
        {
            _logger.Info($"Enter position {axis} between 0 and {boardSize-1}:");
            if (int.TryParse(Console.ReadLine(), out positionValue) && positionValue >= 0)
            {
                try
                {
                    _gameValidator.ValidatePositionValue(positionValue, boardSize);
                    break;
                }
                catch (InvalidPositionException ex)
                {
                    _logger.Error(ex.Message);
                    _logger.Log($"Incorrect position {axis} {positionValue}");
                }
            }
            else
            {
                _logger.Info($"Invalid position {axis}. Please enter a valid number.");
            }
        }
        return positionValue;
    }
}
