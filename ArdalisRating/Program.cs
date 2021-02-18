using System;

namespace ArdalisRating
{
    class Program
    {
        static void Main(string[] args)
        {
            var Logger = new ConsoleLogger();
            Logger.Log("Ardalis Insurance Rating System Starting...");

            var engine = new RatingEngine();
            engine.Rate();

            if (engine.Rating > 0)
            {
                Logger.Log($"Rating: {engine.Rating}");
            }
            else
            {
                Logger.Log("No rating produced.");
            }

        }
    }
}
