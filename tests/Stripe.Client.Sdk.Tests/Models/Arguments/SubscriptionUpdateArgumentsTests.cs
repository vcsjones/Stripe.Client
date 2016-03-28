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
    public class SubscriptionUpdateArgumentsTests
    {
        private SubscriptionUpdateArguments _args;
        public SubscriptionUpdateArgumentsTests()
        {
            _args = GenFu.GenFu.New<SubscriptionUpdateArguments>();
        }

        [Fact]
        public void SubscriptionUpdateArguments_CustomerIdIsRequired()
        {
            // Arrange 
            _args.CustomerId = null;

            // Act
            Func<IEnumerable<KeyValuePair<string, string>>> func = () => StripeClient.GetKeyValuePairs(_args);

            // Assert
            func.Enumerating().ShouldThrow<ValidationException>();
        }

        [Fact]
        public void SubscriptionUpdateArguments_SubscriptionIdIsRequired()
        {
            // Arrange 
            _args.SubscriptionId = null;

            // Act
            Func<IEnumerable<KeyValuePair<string, string>>> func = () => StripeClient.GetKeyValuePairs(_args);

            // Assert
            func.Enumerating().ShouldThrow<ValidationException>();
        }

        [Fact]
        public void SubscriptionUpdateArguments_CardTokenTrumpsCardCreateArguments()
        {
            // Arrange 
            _args.CardCreateArguments = GenFu.GenFu.New<CardCreateArguments>();

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().Contain(x => x.Key == "source" && x.Value == _args.CardToken);
        }

        [Fact]
        public void SubscriptionUpdateArguments_CardCreateArguments()
        {
            // Arrange 
            _args.CardToken = null;
            _args.CardCreateArguments = GenFu.GenFu.New<CardCreateArguments>();
            _args.CardCreateArguments.ExpMonth = 10;
            _args.CardCreateArguments.ExpYear = DateTime.UtcNow.Year;

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().NotContain(x => x.Key == "source")
                .And.Contain(x => x.Key == "source[object]" && x.Value == "card");
        }

        [Fact]
        public void SubscriptionUpdateArguments_GetAllKeys()
        {
            // Arrange 
            _args.ApplicationFeePercent = 1;
            _args.Metadata = Data.Metadata;
            _args.Quantity = 1;
            _args.TaxPercent = 8.2m;
            _args.TrialEnd = DateTime.UtcNow;

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(9)
                .And.Contain(x => x.Key == "application_fee_percent")
                .And.Contain(x => x.Key == "coupon")
                .And.Contain(x => x.Key == "metadata[key1]")
                .And.Contain(x => x.Key == "metadata[key2]")
                .And.Contain(x => x.Key == "plan")
                .And.Contain(x => x.Key == "quantity")
                .And.Contain(x => x.Key == "source")
                .And.Contain(x => x.Key == "tax_percent")
                .And.Contain(x => x.Key == "trial_end");
        }
    }
}