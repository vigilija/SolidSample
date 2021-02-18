namespace ArdalisRating
{
    public abstract class Rater
    {
        protected readonly ConsoleLogger _logger;
        protected readonly RatingEngine _engine;

        public Rater(RatingEngine engine, ConsoleLogger logger)
        {
            _engine = engine;
            _logger = logger;
        }

        public abstract void Rate(Policy policy);
    }
}
