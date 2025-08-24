using EasyJet.KnightsTravel.Application.Exceptions;
using EasyJet.KnightsTravel.Domain.Interfaces;

namespace EasyJet.KnightsTravel.Application.Validators;

public class GameValidator : IGameValidator
{
    public void ValidateBoardSize(int size)
    {
        if (size < 5 || size > 8)
            throw new InvalidBoardSizeException();
    }

    public void ValidatePositionValue(int postionValue, int boardSize)
    {
        if(postionValue > boardSize -1 || postionValue < 0)
            throw new InvalidPositionException(boardSize);
    }
}
