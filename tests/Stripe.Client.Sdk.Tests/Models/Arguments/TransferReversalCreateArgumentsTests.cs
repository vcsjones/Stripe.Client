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
    public class TransferReversalCreateArgumentsTests
    {
        private TransferReversalCreateArguments _args;
        public TransferReversalCreateArgumentsTests()
        {
            _args = GenFu.GenFu.New<TransferReversalCreateArguments>();
        }

        [Fact]
        public void TransferReversalCreateArguments_TranferIdIsRequired()
        {
            // Arrange 
            _args.TransferId = null;

            // Act
            Func<IEnumerable<KeyValuePair<string, string>>> func = () => StripeClient.GetKeyValuePairs(_args);

            // Assert
            func.Enumerating().ShouldThrow<ValidationException>();
        }

        [Fact]
        public void TransferReversalCreateArguments_AmountWhenSet()
        {
            // Arrange 
            _args.Amount = 100;

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().Contain(x => x.Key == "amount");
        }

        [Fact]
        public void TransferReversalCreateArguments_GetAllKeys()
        {
            // Arrange 
            _args.Metadata = Data.Metadata;

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(4)
                .And.Contain(x => x.Key == "description")
                .And.Contain(x => x.Key == "metadata[key1]")
                .And.Contain(x => x.Key == "metadata[key2]")
                .And.Contain(x => x.Key == "refund_application_fee");
        }
    }
}