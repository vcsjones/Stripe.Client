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
    public class ActiveSubscriptionListFilterTests
    {
        private ActiveSubscriptionListFilter _filter;
        public ActiveSubscriptionListFilterTests()
        {
            _filter = GenFu.GenFu.New<ActiveSubscriptionListFilter>();
        }

        [Fact]
        public void ActiveSubscriptionListFilter_CustomerIdRequired()
        {
            // Arrange 
             _filter.CustomerId = null;

            // Act
            Func<IEnumerable<KeyValuePair<string, string>>> func = () => StripeClient.GetKeyValuePairs(_filter);

            // Assert
            func.Enumerating().ShouldThrow<ValidationException>();
        }

        [Fact]
        public void ActiveSubscriptionListFilter_GetAllKeys()
        {
            // Arrange
            _filter.Limit = 10;

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_filter).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(3)
                .And.NotContain(x => x.Key == "customer_id")
                .And.Contain(x => x.Key == "ending_before")
                .And.Contain(x => x.Key == "starting_after")
                .And.Contain(x => x.Key == "limit");
        }
    }
}