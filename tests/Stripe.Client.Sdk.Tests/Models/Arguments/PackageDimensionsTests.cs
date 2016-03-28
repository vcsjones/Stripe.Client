using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FluentAssertions;
using Xunit;
using Stripe.Client.Sdk.Clients;
using Stripe.Client.Sdk.Models.Arguments;

namespace Stripe.Client.Sdk.Tests.Models.Arguments
{
    public class PackageDimensionsTests
    {
        [Fact]
        public void PackageDimensionArguments_GetAllKeys()
        {
            // Arrange 
            var args = new PackageDimensions();
            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(args).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(4)
                .And.Contain(x => x.Key == "height")
                .And.Contain(x => x.Key == "length")
                .And.Contain(x => x.Key == "weight")
                .And.Contain(x => x.Key == "width");
        }
    }
}