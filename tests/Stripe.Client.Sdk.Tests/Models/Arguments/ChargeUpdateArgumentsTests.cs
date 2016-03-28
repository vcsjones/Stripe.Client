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
    public class ChargeUpdateArgumentsTests
    {
        private ChargeUpdateArguments _args;
        public ChargeUpdateArgumentsTests()
        {
            _args = GenFu.GenFu.New<ChargeUpdateArguments>();
        }

        [Fact]
        public void ChargeUpdateArguments_ChargeIdIsRequired()
        {
            // Arrange 
            _args.ChargeId = null;

            // Act
            Func<IEnumerable<KeyValuePair<string, string>>> func = () => StripeClient.GetKeyValuePairs(_args);

            // Assert
            func.Enumerating().ShouldThrow<ValidationException>();
        }

        [Fact]
        public void ChargeUpdateArguments_ShippingWhenSet()
        {
            // Arrange 
            _args.Shipping = GenFu.GenFu.New<ShippingArguments>();
            _args.Shipping.Address = GenFu.GenFu.New<AddressArguments>();
            _args.Shipping.Address.Country = "US";

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().Contain(x => x.Key == "shipping[address][city]")
                .And.Contain(x => x.Key == "shipping[address][country]")
                .And.Contain(x => x.Key == "shipping[address][line1]")
                .And.Contain(x => x.Key == "shipping[address][line2]")
                .And.Contain(x => x.Key == "shipping[address][postal_code]")
                .And.Contain(x => x.Key == "shipping[address][state]")
                .And.Contain(x => x.Key == "shipping[name]")
                .And.Contain(x => x.Key == "shipping[phone]");
        }

        [Fact]
        public void ChargeUpdateArguments_FraudDetailsWhenSet()
        {
            // Arrange 
            _args.FraudDetails = new Dictionary<string, string>
            {
                { "user_report","fradulent"}
            };

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().Contain(x => x.Key == "fraud_details[user_report]" && x.Value == "fradulent");
        }

        [Fact]
        public void ChargeUpdateArguments_GetAllKeys()
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
                .And.Contain(x => x.Key == "receipt_email");
        }
    }
}