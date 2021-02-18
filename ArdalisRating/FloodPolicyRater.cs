using System;
using System.Collections.Generic;
using System.Text;

namespace ArdalisRating
{
    public class FloodPolicyRater : Rater
    {
        public FloodPolicyRater(RatingEngine engine, ConsoleLogger logger) : base(engine, logger)
        {
        }

        public override void Rate(Policy policy)
        {
            _logger.Log("Rating FLOOD policy...");
            _logger.Log("Validating policy.");

            _engine.Rating = (decimal)0.4;
        }
    }
}
