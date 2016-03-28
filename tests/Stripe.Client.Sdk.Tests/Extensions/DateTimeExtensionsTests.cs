using FluentAssertions;
using Xunit;
using Stripe.Client.Sdk.Extensions;
using System;

namespace Stripe.Client.Sdk.Tests.Extensions
{
    public class DateTimeExtensionsTests
    {
        [Fact]
        public void ToDateTimeTest_EpochStart()
        {
            // Arrange
            var epoch = (long)0;

            // Act 
            var date = epoch.ToDateTime();

            // Assert 
            date.Should().HaveYear(1970)
                .And.HaveMonth(1)
                .And.HaveDay(1)
                .And.HaveHour(0)
                .And.HaveMinute(0)
                .And.HaveSecond(0);
        }

        [Fact]
        public void ToEpochTest_EpochStart()
        {
            // Arrange
            var date = new DateTime(1970, 1, 1);

            // Act 
            var epoch = date.ToEpoch();

            // Assert 
            epoch.Should().Be(0, "that is when epoch starts");
        }

        [Fact]
        public void ToDateTimeTest_BeforEpochStart()
        {
            // Arrange
            var epoch = (long)-1;

            // Act 
            var date = epoch.ToDateTime();

            // Assert 
            date.Should().HaveYear(1969)
                .And.HaveMonth(12)
                .And.HaveDay(31)
                .And.HaveHour(23)
                .And.HaveMinute(59)
                .And.HaveSecond(59);
        }

        [Fact]
        public void ToEpochTest_BeforeEpochStart()
        {
            // Arrange
            var date = new DateTime(1969, 12, 31, 23, 59, 59);

            // Act 
            var epoch = date.ToEpoch();

            // Assert 
            epoch.Should().Be(-1, "one second less than epoch start");
        }

        [Fact]
        public void ToEpochToDateTimeTest()
        {
            // Arrange
            var date = DateTime.UtcNow;

            // Act 
            var date2 = date.ToEpoch().ToDateTime();

            // Assert 
            date2.Should().HaveYear(date.Year)
                .And.HaveMonth(date.Month)
                .And.HaveDay(date.Day)
                .And.HaveHour(date.Hour)
                .And.HaveMinute(date.Minute);
        }
    }
}