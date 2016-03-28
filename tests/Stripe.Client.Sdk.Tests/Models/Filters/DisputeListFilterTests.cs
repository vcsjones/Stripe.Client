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
    public class DisputeListFilterTests
    {
        private DisputeListFilter _filter;
        public DisputeListFilterTests()
        {
            _filter = GenFu.GenFu.New<DisputeListFilter>();
        }

        [Fact]
        public void DisputeListFilter_DoesNotHaveRequiredFields()
        {
            // Arrange 
            _filter = new DisputeListFilter();

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_filter).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(0);
        }

        [Fact]
        public void DisputeListFilter_CreatedDateTimeOverridesCreatedFilter()
        {
            // Arrange
            _filter.CreatedDateTime = DateTime.UtcNow;
            _filter.CreatedFilter = Data.DateFilter;

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_filter).ToList();

            // Assert
            keyValuePairs.Should().Contain(x => x.Key == "created")
                .And.NotContain(x => x.Key == "created[gt]")
                .And.NotContain(x => x.Key == "created[gte]")
                .And.NotContain(x => x.Key == "created[lt]")
                .And.NotContain(x => x.Key == "created[lte]");
        }


        [Fact]
        public void DisputeListFilter_CreatedFilter()
        {
            // Arrange
            _filter.CreatedDateTime = null;
            _filter.CreatedFilter = Data.DateFilter;

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_filter).ToList();

            // Assert
            keyValuePairs.Should().NotContain(x => x.Key == "created")
                .And.Contain(x => x.Key == "created[gt]")
                .And.Contain(x => x.Key == "created[gte]")
                .And.Contain(x => x.Key == "created[lt]")
                .And.Contain(x => x.Key == "created[lte]");
        }

        [Fact]
        public void DisputeListFilter_GetAllKeys()
        {
            // Arrange
            _filter.Limit = 10;

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_filter).ToList();

            // Assert
            keyValuePairs.Should().Contain(x => x.Key == "ending_before")
                .And.Contain(x => x.Key == "starting_after")
                .And.Contain(x => x.Key == "limit")
                .And.HaveCount(3);
        }
    }
}