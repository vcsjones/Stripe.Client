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
    public class CustomerBankAccountUpdateArgumentsTests
    {
        private CustomerBankAccountUpdateArguments _args;
        public CustomerBankAccountUpdateArgumentsTests()
        {
            _args = GenFu.GenFu.New<CustomerBankAccountUpdateArguments>();
        }

        [Fact]
        public void CustomerBankAccountUpdateArguments_BankAccountIdIsRequired()
        {
            // Arrange 
            _args.BankAccountId = null;

            // Act
            Func<IEnumerable<KeyValuePair<string, string>>> func = () => StripeClient.GetKeyValuePairs(_args);

            // Assert
            func.Enumerating().ShouldThrow<ValidationException>();
        }

        [Fact]
        public void CustomerBankAccountUpdateArguments_CustomerIdIsRequired()
        {
            // Arrange 
            _args.CustomerId = null;

            // Act
            Func<IEnumerable<KeyValuePair<string, string>>> func = () => StripeClient.GetKeyValuePairs(_args);

            // Assert
            func.Enumerating().ShouldThrow<ValidationException>();
        }


        [Fact]
        public void CustomerBankAccountUpdateArguments_GetAllKeys()
        {
            // Arrange 
            _args.Metadata = Data.Metadata;

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(3)
                .And.Contain(x => x.Key == "default_for_currency")
                .And.Contain(x => x.Key == "metadata[key1]")
                .And.Contain(x => x.Key == "metadata[key2]");
        }
    }
}