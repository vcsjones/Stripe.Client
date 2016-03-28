using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FluentAssertions;
using Xunit;
using Stripe.Client.Sdk.Clients;
using Stripe.Client.Sdk.Models.Filters;

namespace Stripe.Client.Sdk.Tests.Models.Arguments
{
    public class AccountRejectArgumentsTests
    {
        private AccountRejectArguments _args;
        public AccountRejectArgumentsTests()
        {
            _args = GenFu.GenFu.New<AccountRejectArguments>();
        }

        [Fact]
        public void AccountRejectArguments_AccountIdIsRequired()
        {
            // Arrange 
            _args.AccountId = null;

            // Act
            Func<IEnumerable<KeyValuePair<string, string>>> func = () => StripeClient.GetKeyValuePairs(_args);

            // Assert
            func.Enumerating().ShouldThrow<ValidationException>();
        }

        [Fact]
        public void AccountRejectArguments_ReasonIsRequired()
        {
            // Arrange 
            _args.Reason = null;

            // Act
            Func<IEnumerable<KeyValuePair<string, string>>> func = () => StripeClient.GetKeyValuePairs(_args);

            // Assert
            func.Enumerating().ShouldThrow<ValidationException>();
        }

        [Fact]
        public void AccountRejectArguments_GetAllKeys()
        {
            // Arrange 

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(1).And.Contain(x => x.Key == "reason");
        }
    }
}