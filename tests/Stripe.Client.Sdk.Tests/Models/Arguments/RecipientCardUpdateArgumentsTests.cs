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
    public class RecipientCardUpdateArgumentsTests
    {
        private RecipientCardUpdateArguments _args;
        public RecipientCardUpdateArgumentsTests()
        {
            _args = GenFu.GenFu.New<RecipientCardUpdateArguments>();
        }

        [Fact]
        public void RecipientCardUpdateArguments_CardIdIsRequired()
        {
            // Arrange 
            _args.CardId = null;

            // Act
            Func<IEnumerable<KeyValuePair<string, string>>> func = () => StripeClient.GetKeyValuePairs(_args);

            // Assert
            func.Enumerating().ShouldThrow<ValidationException>();
        }

        [Fact]
        public void RecipientCardUpdateArguments_AccountIdIsRequired()
        {
            // Arrange 
            _args.RecipientId = null;

            // Act
            Func<IEnumerable<KeyValuePair<string, string>>> func = () => StripeClient.GetKeyValuePairs(_args);

            // Assert
            func.Enumerating().ShouldThrow<ValidationException>();
        }


        [Fact]
        public void RecipientCardUpdateArguments_GetOtherKeys()
        {
            // Arrange 
            _args.Metadata = Data.Metadata;

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(11)
                .And.Contain(x => x.Key == "metadata[key1]")
                .And.Contain(x => x.Key == "metadata[key2]")
                .And.Contain(x => x.Key == "address_city")
                .And.Contain(x => x.Key == "address_line1")
                .And.Contain(x => x.Key == "address_line2")
                .And.Contain(x => x.Key == "address_state")
                .And.Contain(x => x.Key == "address_zip")
                .And.Contain(x => x.Key == "exp_month")
                .And.Contain(x => x.Key == "exp_year")
                .And.Contain(x => x.Key == "name");
        }
    }
}