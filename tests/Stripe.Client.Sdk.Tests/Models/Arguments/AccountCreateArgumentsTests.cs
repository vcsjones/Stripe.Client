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
    public class AccountCreateArgumentsTests
    {
        private AccountCreateArguments _args;
        public AccountCreateArgumentsTests()
        {
            _args = GenFu.GenFu.New<AccountCreateArguments>();
        }

        [Fact]
        public void AccountCreateArguments_DoesNotHaveRequiredFields()
        {
            // Arrange 
            _args = new AccountCreateArguments();

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(0);
        }

        [Fact]
        public void CrateAccountArguments_RequiresValidEmail()
        {
            var args = new AccountCreateArguments
            {
                Email = "notvalidemail"
            };

            Func<IEnumerable<KeyValuePair<string, string>>> func = () => StripeClient.GetKeyValuePairs(args);
            func.Enumerating().ShouldThrow<ValidationException>();
        }

        [Fact]
        public void AccountCreateArguments_GetAllKeys()
        {
            // Arrange 
            _args.Managed = false;

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(3)
                .And.Contain(x => x.Key == "country")
                .And.Contain(x => x.Key == "email")
                .And.Contain(x => x.Key == "managed");
        }
    }
}