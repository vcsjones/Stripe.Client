using System.Linq;
using FluentAssertions;
using Xunit;
using Stripe.Client.Sdk.Clients;
using Stripe.Client.Sdk.Models.Arguments;

namespace Stripe.Client.Sdk.Tests.Models.Arguments
{
    public class RecipientUpdateArgumentsTests
    {
        private RecipientUpdateArguments _args;
        public RecipientUpdateArgumentsTests()
        {
            _args = GenFu.GenFu.New<RecipientUpdateArguments>();
        }

        [Fact]
        public void RecipientUpdateArguments_DoesNotHaveRequiredFields()
        {
            // Arrange 
            _args = new RecipientUpdateArguments();

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(0);
        }

        [Fact]
        public void RecipientUpdateArguments_BankAccountTokenTrumpsBankAccountCreateArguments()
        {
            // Arrange 
            _args.RecipientBankAccountArguments = GenFu.GenFu.New<RecipientBankAccountArguments>();

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().Contain(x => x.Key == "bank_account" && x.Value == _args.BankAccountToken);
        }

        [Fact]
        public void RecipientUpdateArguments_BankAccountCreateArguments()
        {
            // Arrange 
            _args.BankAccountToken = null;
            _args.RecipientBankAccountArguments = GenFu.GenFu.New<RecipientBankAccountArguments>();

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().NotContain(x => x.Key == "bank_account")
                .And.Contain(x => x.Key == "bank_account[account_number]")
                .And.Contain(x => x.Key == "bank_account[country]")
                .And.Contain(x => x.Key == "bank_account[routing_number]");
        }

        [Fact]
        public void RecipientUpdateArguments_CardTokenTrumpsCardCreateArguments()
        {
            // Arrange 
            _args.CardCreateArguments = GenFu.GenFu.New<CardCreateArguments>();

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().Contain(x => x.Key == "card" && x.Value == _args.CardToken);
        }

        [Fact]
        public void RecipientUpdateArguments_CardCreateArguments()
        {
            // Arrange 
            _args.CardToken = null;
            _args.CardCreateArguments = GenFu.GenFu.New<CardCreateArguments>();
            _args.CardCreateArguments.ExpMonth = 2;
            _args.CardCreateArguments.ExpYear = 2012;

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().NotContain(x => x.Key == "card")
                .And.Contain(x => x.Key == "card[object]" && x.Value == "card");
        }

        [Fact]
        public void RecipientUpdateArguments_GetOtherKeys()
        {
            // Arrange 
            _args.Metadata = Data.Metadata;

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(8)
                .And.Contain(x => x.Key == "bank_account")
                .And.Contain(x => x.Key == "card")
                .And.Contain(x => x.Key == "description")
                .And.Contain(x => x.Key == "email")
                .And.Contain(x => x.Key == "metadata[key1]")
                .And.Contain(x => x.Key == "metadata[key2]")
                .And.Contain(x => x.Key == "name")
                .And.Contain(x => x.Key == "tax_id");
        }
    }
}