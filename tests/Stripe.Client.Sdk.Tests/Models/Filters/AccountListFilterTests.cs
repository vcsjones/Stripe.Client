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
    public class AccountListFilterTests
    {
        private AccountListFilter _filter;
        public AccountListFilterTests()
        {
            _filter = GenFu.GenFu.New<AccountListFilter>();
        }

        [Fact]
        public void AccountListFilter_DoesNotHaveRequiredFields()
        {
            // Arrange 
            _filter = new AccountListFilter();

            // Act
            Func<IEnumerable<KeyValuePair<string, string>>> func = () => StripeClient.GetKeyValuePairs(_filter);

            // Assert
            func.Enumerating().ShouldNotThrow();
        }

        [Fact]
        public void AccountListFilter_GetAllKeys()
        {
            // Arrange
            _filter.Limit = 10;

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_filter).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(3)
                .And.Contain(x => x.Key == "ending_before")
                .And.Contain(x => x.Key == "starting_after")
                .And.Contain(x => x.Key == "limit");
        }
    }
}

