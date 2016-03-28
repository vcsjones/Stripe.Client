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
    public class RefundListFilterTests
    {
        private RefundListFilter _filter;
        public RefundListFilterTests()
        {
            _filter = GenFu.GenFu.New<RefundListFilter>();
        }

        [Fact]
        public void RefundListFilter_DoesNotHaveRequiredFields()
        {
            // Arrange 
            _filter = new RefundListFilter();

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_filter).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(0);
        }

        [Fact]
        public void RefundListFilter_GetAllKeys()
        {
            // Arrange
            _filter.Limit = 10;

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_filter).ToList();

            // Assert
            keyValuePairs.Should().Contain(x => x.Key == "charge")
                .And.Contain(x => x.Key == "ending_before")
                .And.Contain(x => x.Key == "starting_after")
                .And.Contain(x => x.Key == "limit").And.HaveCount(4);
        }
    }
}