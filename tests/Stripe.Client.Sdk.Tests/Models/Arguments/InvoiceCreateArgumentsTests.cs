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
    public class InvoiceCreateArgumentsTests
    {
        private InvoiceCreateArguments _args;
        public InvoiceCreateArgumentsTests()
        {
            _args = GenFu.GenFu.New<InvoiceCreateArguments>();
        }

        [Fact]
        public void InvoiceCreateArguments_CustomerIdIsRequired()
        {
            // Arrange 
            _args.Customer = null;

            // Act
            Func<IEnumerable<KeyValuePair<string, string>>> func = () => StripeClient.GetKeyValuePairs(_args);

            // Assert
            func.Enumerating().ShouldThrow<ValidationException>();
        }

        [Fact]
        public void InvoiceCreateArguments_GetAllKeys()
        {
            // Arrange 
            _args.Metadata = Data.Metadata;
            _args.ApplicationFee = 15;
            _args.TaxPercent = 16;

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(8)
                .And.Contain(x => x.Key == "application_fee")
                .And.Contain(x => x.Key == "customer")
                .And.Contain(x => x.Key == "description")
                .And.Contain(x => x.Key == "metadata[key1]")
                .And.Contain(x => x.Key == "metadata[key2]")
                .And.Contain(x => x.Key == "statement_descriptor")
                .And.Contain(x => x.Key == "subscription")
                .And.Contain(x => x.Key == "tax_percent");
        }
    }
}