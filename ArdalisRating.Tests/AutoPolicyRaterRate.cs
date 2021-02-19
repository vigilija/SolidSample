﻿using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ArdalisRating.Tests
{
    public class FakeLogger : ILogger
    {
        public List<string> LoggedMessages { get; } = new List<string>();
        public void Log(string message)
        {
            LoggedMessages.Add(message);
        }
    }

    public class FakeRatingUpdater : IRatingUpdater
    {
        public decimal? NewRating { get; set; }
        public void UpdateRating(decimal rating)
        {
            NewRating = rating;
        }
    }

    public class AutoPolicyRaterRate
    {
        [Fact]
        public void LogsMakeRequiredMessageGivenPolicyWithoutMakeMoq()
        {
            var mock = new Mock<ILogger>();

            var policy = new Policy() { Type = "Auto" };
            //var logger = new FakeLogger();
            var rater = new AutoPolicyRater(null);

            rater.Logger = mock.Object;
            rater.Rate(policy);
            mock.Verify(m => m.Log("Auto policy must specify Make"), Times.Once());

        }

        [Fact]
        public void LogsMakeRequiredMessageGivenPolicyWithoutMake()
        {
            var policy = new Policy() { Type = "Auto" };
            var logger = new FakeLogger();
            var rater = new AutoPolicyRater(null);

            rater.Logger = logger;
            rater.Rate(policy);
            Assert.Equal("Auto policy must specify Make", logger.LoggedMessages.Last());
        }

        [Fact]
        public void SetRatingTo1000ForBMWWith250Deductible()
        {
            var policy = new Policy()
            {
                Type = "Auto",
                Make = "BMW",
                Deductible = 250m
            };
            var ratingUpdater = new FakeRatingUpdater();
            var rater = new AutoPolicyRater(ratingUpdater);

            rater.Rate(policy);

            Assert.Equal(1000m, ratingUpdater.NewRating.Value);
        }

        [Fact]
        public void SetRatingTo1000ForBMWWith250DeductibleMoq()
        {
            var policy = new Policy()
            {
                Type = "Auto",
                Make = "BMW",
                Deductible = 250m
            };

            var moq = new Mock<IRatingUpdater>();
            var rater = new AutoPolicyRater(moq.Object);

            rater.Rate(policy);

            moq.Verify(moq => moq.UpdateRating(1000m), Times.Once());
        }
    }
}
