namespace EasyJet.KnightsTravel.Application.Exceptions;

public class InvalidPositionException : Exception
{
    public InvalidPositionException(int size)
        : base($"Position must be between 0 and {size-1}")
    {
        
    }
}
