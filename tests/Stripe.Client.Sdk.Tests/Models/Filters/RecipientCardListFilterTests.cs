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
    public class RecipientCardListFilterTests
    {
        private RecipientCardListFilter _filter;
        public RecipientCardListFilterTests()
        {
            _filter = GenFu.GenFu.New<RecipientCardListFilter>();
        }

        [Fact]
        public void RecipientCardListFilter_RecipientIdIsRequired()
        {
            // Arrange 
            _filter.RecipientId = null;

            // Act
            Func<IEnumerable<KeyValuePair<string, string>>> func = () => StripeClient.GetKeyValuePairs(_filter);

            // Assert
            func.Enumerating().ShouldThrow<ValidationException>();
        }

        [Fact]
        public void RecipientCardListFilter_GetAllKeys()
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
