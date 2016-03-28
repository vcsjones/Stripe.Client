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
    public class InvoiceItemCreateArgumentsTests
    {
        private InvoiceItemCreateArguments _args;
        public InvoiceItemCreateArgumentsTests()
        {
            GenFu.GenFu.Configure<InvoiceItemCreateArguments>().Fill(x => x.Amount, () => 30);
            _args = GenFu.GenFu.New<InvoiceItemCreateArguments>();
        }

        [Fact]
        public void InvoiceItemCreateArguments_CurrencyIsRequired()
        {
            // Arrange 
            _args.Currency = null;

            // Act
            Func<IEnumerable<KeyValuePair<string, string>>> func = () => StripeClient.GetKeyValuePairs(_args);

            // Assert
            func.Enumerating().ShouldThrow<ValidationException>();
        }

        [Fact]
        public void InvoiceItemCreateArguments_CustomerIsRequired()
        {
            // Arrange 
            _args.Customer = null;

            // Act
            Func<IEnumerable<KeyValuePair<string, string>>> func = () => StripeClient.GetKeyValuePairs(_args);

            // Assert
            func.Enumerating().ShouldThrow<ValidationException>();
        }

        [Fact]
        public void InvoiceItemCreateArguments_GetAllKeys()
        {
            // Arrange 
            _args.Metadata = Data.Metadata;
            _args.Amount = 100;
            _args.Discountable = false;

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(9)
                .And.Contain(x => x.Key == "amount")
                .And.Contain(x => x.Key == "currency")
                .And.Contain(x => x.Key == "customer")
                .And.Contain(x => x.Key == "description")
                .And.Contain(x => x.Key == "discountable")
                .And.Contain(x => x.Key == "invoice")
                .And.Contain(x => x.Key == "metadata[key1]")
                .And.Contain(x => x.Key == "metadata[key2]")
                .And.Contain(x => x.Key == "subscription");
        }
    }
}