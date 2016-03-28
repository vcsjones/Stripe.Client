using System.Linq;
using FluentAssertions;
using Xunit;
using Stripe.Client.Sdk.Clients;
using Stripe.Client.Sdk.Models.Arguments;

namespace Stripe.Client.Sdk.Tests.Models.Arguments
{
    public class BankAccountTokenCreateArgumentsTests
    {

        private BankAccountTokenCreateArguments _args;
        public BankAccountTokenCreateArgumentsTests()
        {
            _args = GenFu.GenFu.New<BankAccountTokenCreateArguments>();
        }

        [Fact]
        public void BankAccountTokenArguments_GetAllKeys()
        {
            // Arrange 
            _args.BankAccount = GenFu.GenFu.New<BankAccountTokenArguments>();

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(7)
                .And.Contain(x => x.Key == "bank_account[account_number]")
                .And.Contain(x => x.Key == "bank_account[country]")
                .And.Contain(x => x.Key == "bank_account[currency]")
                .And.Contain(x => x.Key == "bank_account[account_holder_name]")
                .And.Contain(x => x.Key == "bank_account[account_holder_type]")
                .And.Contain(x => x.Key == "bank_account[routing_number]")
                .And.Contain(x => x.Key == "customer");
        }
    }
}