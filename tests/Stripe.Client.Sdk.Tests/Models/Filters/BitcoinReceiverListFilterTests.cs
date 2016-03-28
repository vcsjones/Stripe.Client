using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FluentAssertions;
using Xunit;
using Stripe.Client.Sdk.Clients;
using Stripe.Client.Sdk.Models.Filters;

namespace Stripe.Client.Sdk.Tests.Models.Filters
{
    public class BitcoinReceiverListFilterTests
    {
        private BitcoinReceiverListFilter _filter;
        public BitcoinReceiverListFilterTests()
        {
            _filter = GenFu.GenFu.New<BitcoinReceiverListFilter>();
        }

        [Fact]
        public void BitcoinReceiverListFilter_DoesNotHaveRequiredFields()
        {
            // Arrange 
            _filter = new BitcoinReceiverListFilter();

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_filter).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(0);
        }

        [Fact]
        public void BitcoinReceiverListFilter_GetAllKeys()
        {
            // Arrange
            _filter.Limit = 10;
            _filter.Active = true;
            _filter.Filled = true;

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_filter).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(5)
                .And.Contain(x => x.Key == "active")
                .And.Contain(x => x.Key == "filled")
                .And.Contain(x => x.Key == "ending_before")
                .And.Contain(x => x.Key == "starting_after")
                .And.Contain(x => x.Key == "limit");
        }
    }
}