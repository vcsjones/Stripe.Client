using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using FluentAssertions;
using Xunit;
using Stripe.Client.Sdk.Clients;
using Stripe.Client.Sdk.Models.Filters;

namespace Stripe.Client.Sdk.Tests.Models.Filters
{
    public class BalanceTransactionListFilterTests
    {
        private BalanceTransactionListFilter _filter;
        public BalanceTransactionListFilterTests()
        {
            _filter = GenFu.GenFu.New<BalanceTransactionListFilter>();
        }

        [Fact]
        public void BalanceTransactionListFilter_DoesNotHaveRequiredFields()
        {
            // Arrange 
            _filter = new BalanceTransactionListFilter();

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_filter).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(0);
        }


        [Fact]
        public void BalanceTransactionListFilter_AvailableOnDateTimeOverridesAvailableOnFilter()
        {
            // Arrange
            _filter.AvailableOnDateTime = DateTime.UtcNow;
            _filter.AvailableOnFilter = Data.DateFilter;

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_filter).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(7, string.Join("\r\n", keyValuePairs.Select(x => x.Key + " : " + x.Value)))
                .And.Contain(x => x.Key == "currency")
                .And.Contain(x => x.Key == "source")
                .And.Contain(x => x.Key == "transfer")
                .And.Contain(x => x.Key == "type")
                .And.Contain(x => x.Key == "available_on")
                .And.Contain(x => x.Key == "ending_before")
                .And.Contain(x => x.Key == "starting_after");
        }

        [Fact]
        public void BalanceTransactionListFilter_CreatedDateTimeOverridesCreatedFilter()
        {
            // Arrange
            _filter.CreatedDateTime = DateTime.UtcNow;
            _filter.CreatedFilter = Data.DateFilter;

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_filter).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(7, string.Join("\r\n", keyValuePairs.Select(x => x.Key + " : " + x.Value)))
                .And.Contain(x => x.Key == "currency")
                .And.Contain(x => x.Key == "source")
                .And.Contain(x => x.Key == "transfer")
                .And.Contain(x => x.Key == "type")
                .And.Contain(x => x.Key == "created")
                .And.Contain(x => x.Key == "ending_before")
                .And.Contain(x => x.Key == "starting_after");
        }



        [Fact]
        public void BalanceTransactionListFilter_GetAllKeys()
        {
            // Arrange
            _filter.Limit = 10;
            _filter.AvailableOnDateTime = null;
            _filter.AvailableOnFilter = Data.DateFilter;


            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_filter).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(11, string.Join("\r\n", keyValuePairs.Select(x => x.Key + " : " + x.Value)))
                .And.Contain(x => x.Key == "currency")
                .And.Contain(x => x.Key == "source")
                .And.Contain(x => x.Key == "transfer")
                .And.Contain(x => x.Key == "type")
                .And.Contain(x => x.Key == "available_on[gt]")
                .And.Contain(x => x.Key == "available_on[gte]")
                .And.Contain(x => x.Key == "available_on[lt]")
                .And.Contain(x => x.Key == "available_on[lte]")
                .And.Contain(x => x.Key == "ending_before")
                .And.Contain(x => x.Key == "starting_after");
        }
    }
}