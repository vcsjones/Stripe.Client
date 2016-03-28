using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FluentAssertions;
using Xunit;
using Stripe.Client.Sdk.Clients;
using Stripe.Client.Sdk.Models.Arguments;

namespace Stripe.Client.Sdk.Tests.Models.Arguments
{
    public class AccountCardCreateArgumentsTests
    {
        private AccountCardCreateArguments _args;
        public AccountCardCreateArgumentsTests()
        {
            _args = GenFu.GenFu.New<AccountCardCreateArguments>();
        }

        [Fact]
        public void AccountCardCreateArguments_ExternalAccountIsRequired()
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
        public void AccountCardCreateArguments_CardTokenTrumpsCardCreateArguments()
        {
            // Arrange 
            _args.CardCreateArguments = GenFu.GenFu.New<CardCreateArguments>();

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().Contain(x => x.Key == "external_account" && x.Value == _args.CardToken);
        }

        [Fact]
        public void AccountCardCreateArguments_CardCreateArguments()
        {
            // Arrange 
            _args.CardToken = null;
            _args.CardCreateArguments = GenFu.GenFu.New<CardCreateArguments>();
            _args.CardCreateArguments.ExpMonth = 8;
            _args.CardCreateArguments.ExpYear = DateTime.UtcNow.Year;

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().NotContain(x => x.Key == "external_account")
                .And.Contain(x => x.Key == "external_account[object]" && x.Value == "card");
        }

        [Fact]
        public void AccountCardCreateArguments_GetOtherKeys()
        {
            // Arrange 
            _args.DefaultForCurrency = true;
            _args.Metadata = Data.Metadata;

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(4)
                .And.Contain(x => x.Key == "external_account")
                .And.Contain(x => x.Key == "default_for_currency")
                .And.Contain(x => x.Key == "metadata[key1]")
                .And.Contain(x => x.Key == "metadata[key2]");
        }
    }
}