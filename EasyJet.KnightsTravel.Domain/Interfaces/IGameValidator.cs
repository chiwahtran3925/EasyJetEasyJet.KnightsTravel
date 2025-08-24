namespace EasyJet.KnightsTravel.Domain.Interfaces;

public interface IGameValidator
{
    public void ValidateBoardSize(int size);
    public void ValidatePositionValue(int postionValue, int boardSize);
}
