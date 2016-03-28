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
    public class BankAccountCreateArgumentsTests
    {
        private BankAccountCreateArguments _args;
        public BankAccountCreateArgumentsTests()
        {
            _args = GenFu.GenFu.New<BankAccountCreateArguments>();
        }

        [Fact]
        public void BankAccountCreateArguments_AccountNumberIsRequired()
        {
            // Arrange 
            _args.AccountNumber = null;

            // Act
            Func<IEnumerable<KeyValuePair<string, string>>> func = () => StripeClient.GetKeyValuePairs(_args);

            // Assert
            func.Enumerating().ShouldThrow<ValidationException>();
        }

        [Fact]
        public void BankAccountCreateArguments_CountryIsRequired()
        {
            // Arrange 
            _args.Country = null;

            // Act
            Func<IEnumerable<KeyValuePair<string, string>>> func = () => StripeClient.GetKeyValuePairs(_args);

            // Assert
            func.Enumerating().ShouldThrow<ValidationException>();
        }

        [Fact]
        public void BankAccountCreateArguments_CurrencyIsRequired()
        {
            // Arrange 
            _args.Currency = null;

            // Act
            Func<IEnumerable<KeyValuePair<string, string>>> func = () => StripeClient.GetKeyValuePairs(_args);

            // Assert
            func.Enumerating().ShouldThrow<ValidationException>();
        }


        [Fact]
        public void BankAccountCreateArguments_GetAllKeys()
        {
            // Arrange 

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(7)
                .And.Contain(x => x.Key == "object" && x.Value == "bank_account")
                .And.Contain(x => x.Key == "account_number")
                .And.Contain(x => x.Key == "country")
                .And.Contain(x => x.Key == "currency")
                .And.Contain(x => x.Key == "account_holder_name")
                .And.Contain(x => x.Key == "account_holder_type")
                .And.Contain(x => x.Key == "routing_number");
        }
    }
}