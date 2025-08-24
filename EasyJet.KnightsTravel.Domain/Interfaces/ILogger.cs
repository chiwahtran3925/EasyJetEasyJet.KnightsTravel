
namespace EasyJet.KnightsTravel.Domain.Interfaces;

public interface ILogger
{

    public void Log(string message);

    public void Info(string message);

    public void Visual(string message, bool newLine = false);

    public void Error(string message);
}