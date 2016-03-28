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
    public class SkuListFilterTests
    {
        private SkuListFilter _filter;
        public SkuListFilterTests()
        {
            _filter = GenFu.GenFu.New<SkuListFilter>();
        }

        [Fact]
        public void SkuListFilter_DoesNotHaveRequiredFields()
        {
            // Arrange 
            _filter = new SkuListFilter();

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_filter).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(0);
        }

        [Fact]
        public void SkuListFilter_GetAllKeys()
        {
            // Arrange
            _filter.Limit = 10;
            _filter.Active = true;
            _filter.InStock = true;
            _filter.Attributes = new Dictionary<string, string>
            {
                {"color", "red"},
                {"size", "medium"}
            };

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_filter).ToList();

            // Assert
            keyValuePairs.Should().Contain(x => x.Key == "active")
                .And.Contain(x => x.Key == "in_stock")
                .And.Contain(x => x.Key == "product")
                .And.Contain(x => x.Key == "attributes[color]" && x.Value == "red")
                .And.Contain(x => x.Key == "attributes[size]" && x.Value == "medium")
                .And.Contain(x => x.Key == "ending_before")
                .And.Contain(x => x.Key == "starting_after")
                .And.Contain(x => x.Key == "limit")
                .And.HaveCount(8);
        }
    }
}