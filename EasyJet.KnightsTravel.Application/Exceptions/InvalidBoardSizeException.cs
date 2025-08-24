namespace EasyJet.KnightsTravel.Application.Exceptions;

public class InvalidBoardSizeException : Exception
{
    public InvalidBoardSizeException()
        :base($"Board size must be between 5-8")
    {
        
    }
}
