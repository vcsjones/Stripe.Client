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
    public class OrderUpdateArgumentsTests
    {
        private OrderUpdateArguments _args;
        public OrderUpdateArgumentsTests()
        {
            _args = GenFu.GenFu.New<OrderUpdateArguments>();
        }

        [Fact]
        public void OrderUpdateArguments_OrderIdIsRequired()
        {
            // Arrange 
            _args.OrderId = null;

            // Act
            Func<IEnumerable<KeyValuePair<string, string>>> func = () => StripeClient.GetKeyValuePairs(_args);

            // Assert
            func.Enumerating().ShouldThrow<ValidationException>();
        }

        [Fact]
        public void OrderUpdateArguments_GetAllKeys()
        {
            // Arrange 
            _args.Metadata = Data.Metadata;

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(5)
                .And.Contain(x => x.Key == "coupon")
                .And.Contain(x => x.Key == "metadata[key1]")
                .And.Contain(x => x.Key == "metadata[key2]")
                .And.Contain(x => x.Key == "selected_shipping_method")
                .And.Contain(x => x.Key == "status");
        }
    }
}