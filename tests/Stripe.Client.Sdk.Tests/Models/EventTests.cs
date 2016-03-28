using System.IO;
using FluentAssertions;
using Xunit;
using Newtonsoft.Json;
using Stripe.Client.Sdk.Models;

namespace Stripe.Client.Sdk.Tests.Models
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    public class EventTests
    {
        [Fact]
        public void Event_Parsing()
        {
            // Arrange
            var json = File.ReadAllText("JSON/event.json");

            // Act
            var obj = JsonConvert.DeserializeObject<Event>(json);

            // Assert
            obj.Should().BeAssignableTo<Event>();
            obj.Data.Object.Should().BeAssignableTo<Invoice>();
        }
    }
}
