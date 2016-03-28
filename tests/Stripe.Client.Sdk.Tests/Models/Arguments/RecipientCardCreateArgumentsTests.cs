using FluentAssertions;
using Xunit;
using Stripe.Client.Sdk.Clients;
using Stripe.Client.Sdk.Models.Arguments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Stripe.Client.Sdk.Tests.Models.Arguments
{
    public class RecipientCardCreateArgumentsTests
    {
        private RecipientCardCreateArguments _args;
        public RecipientCardCreateArgumentsTests()
        {
            _args = GenFu.GenFu.New<RecipientCardCreateArguments>();
        }

        [Fact]
        public void RecipientCardCreateArguments_CardIsRequired()
        {
            // Arrange 
            _args.CardToken = null;
            _args.CardCreateArguments = null;

            // Act
            Func<IEnumerable<KeyValuePair<string, string>>> func = () => StripeClient.GetKeyValuePairs(_args);

            // Assert
            func.Enumerating().ShouldThrow<ValidationException>();
        }

        [Fact]
        public void RecipientCardCreateArguments_CardTokenTrumpsCardCreateArguments()
        {
            // Arrange 
            _args.CardCreateArguments = GenFu.GenFu.New<CardCreateArguments>();

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().Contain(x => x.Key == "card" && x.Value == _args.CardToken);
        }

        [Fact]
        public void RecipientCardCreateArguments_CardCreateArguments()
        {
            // Arrange 
            _args.CardToken = null;
            _args.CardCreateArguments = GenFu.GenFu.New<CardCreateArguments>();
            _args.CardCreateArguments.ExpMonth = 9;
            _args.CardCreateArguments.ExpYear = DateTime.UtcNow.Year;


            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().NotContain(x => x.Key == "card")
                .And.Contain(x => x.Key == "card[object]" && x.Value == "card");
        }

        [Fact]
        public void RecipientCardCreateArguments_GetOtherKeys()
        {
            // Arrange 
            _args.Metadata = Data.Metadata;

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(4)
                .And.Contain(x => x.Key == "card")
                .And.Contain(x => x.Key == "metadata[key1]")
                .And.Contain(x => x.Key == "metadata[key2]");
        }
    }
}