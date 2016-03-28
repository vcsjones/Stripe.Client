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
    public class ProductListFilterTests
    {
        private ProductListFilter _filter;
        public ProductListFilterTests()
        {
            _filter = GenFu.GenFu.New<ProductListFilter>();
        }

        [Fact]
        public void ProductListFilter_DoesNotHaveRequiredFields()
        {
            // Arrange 
            _filter = new ProductListFilter();

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_filter).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(0);
        }

        [Fact]
        public void ProductListFilter_GetAllKeys()
        {
            // Arrange
            _filter.Limit = 10;
            _filter.Active = true;
            _filter.Shippable = true;

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_filter).ToList();

            // Assert
            keyValuePairs.Should().Contain(x => x.Key == "active")
                .And.Contain(x => x.Key == "shippable")
                .And.Contain(x => x.Key == "url")
                .And.Contain(x => x.Key == "ending_before")
                .And.Contain(x => x.Key == "starting_after")
                .And.Contain(x => x.Key == "limit")
                .And.HaveCount(6);
        }
    }
}