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
    public class AddressArgumentsTests
    {
        private AddressArguments _args;
        public AddressArgumentsTests()
        {
            _args = GenFu.GenFu.New<AddressArguments>();
        }

        [Fact]
        public void AddressArguments_DoesNotHaveRequiredFields()
        {
            // Arrange 
            _args = new AddressArguments();

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(0);
        }

        [Fact]
        public void AddressArguments_GetAllKeys()
        {
            // Arrange 
            _args.Country = "US";

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().Contain(x => x.Key == "city")
                .And.Contain(x => x.Key == "country")
                .And.Contain(x => x.Key == "line1")
                .And.Contain(x => x.Key == "line2")
                .And.Contain(x => x.Key == "postal_code")
                .And.Contain(x => x.Key == "state")
                .And.Contain(x => x.Key == "town");
        }
    }
}