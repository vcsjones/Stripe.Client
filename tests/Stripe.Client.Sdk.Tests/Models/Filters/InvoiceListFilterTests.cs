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
    public class InvoiceListFilterTests
    {
        private InvoiceListFilter _filter;
        public InvoiceListFilterTests()
        {
            _filter = GenFu.GenFu.New<InvoiceListFilter>();
        }

        [Fact]
        public void InvoiceListFilter_DoesNotHaveRequiredFields()
        {
            // Arrange 
            _filter = new InvoiceListFilter();

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_filter).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(0);
        }

        [Fact]
        public void InvoiceListFilter_InvoiceDateTimeOverridesCreatedFilter()
        {
            // Arrange
            _filter.InvoiceDateTime = DateTime.UtcNow;
            _filter.InvoiceDateFilter = Data.DateFilter;

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_filter).ToList();

            // Assert
            keyValuePairs.Should().Contain(x => x.Key == "date")
                .And.NotContain(x => x.Key == "date[gt]")
                .And.NotContain(x => x.Key == "date[gte]")
                .And.NotContain(x => x.Key == "date[lt]")
                .And.NotContain(x => x.Key == "date[lte]");
        }


        [Fact]
        public void InvoiceListFilter_InvoiceDateFilter()
        {
            // Arrange
            _filter.InvoiceDateTime = null;
            _filter.InvoiceDateFilter = Data.DateFilter;

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_filter).ToList();

            // Assert
            keyValuePairs.Should().NotContain(x => x.Key == "date")
                .And.Contain(x => x.Key == "date[gt]")
                .And.Contain(x => x.Key == "date[gte]")
                .And.Contain(x => x.Key == "date[lt]")
                .And.Contain(x => x.Key == "date[lte]");
        }

        [Fact]
        public void InvoiceListFilter_GetAllKeys()
        {
            // Arrange
            _filter.Limit = 10;

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_filter).ToList();

            // Assert
            keyValuePairs.Should().Contain(x => x.Key == "customer")
                .And.Contain(x => x.Key == "ending_before")
                .And.Contain(x => x.Key == "starting_after")
                .And.Contain(x => x.Key == "limit")
                .And.HaveCount(4);
        }
    }
}