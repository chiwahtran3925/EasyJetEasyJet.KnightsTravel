using EasyJet.KnightsTravel.Domain.Interfaces;

namespace EasyJet.KnightsTravel.Infrastructure.Logger;

public class Logger : ILogger
{
    public bool Verbose { get; set; } = false;

    public void Log(string message)
    {
        if (Verbose)
            Console.WriteLine($"[LOG] {message}");
    }

    public void Info(string message)
    {
        Console.WriteLine(Verbose ? $"[INFO] {message}" : message);
    }

    public void Visual(string message, bool newLine = false)
    {
        if (newLine)
            Console.WriteLine(message);
        else
            Console.Write(message);
    }

    public void Error(string message)
    {
        Console.WriteLine(Verbose ? $"[ERROR] {message}" : message);
    }
}