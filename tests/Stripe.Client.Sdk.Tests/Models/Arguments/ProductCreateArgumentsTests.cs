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
    public class ProductCreateArgumentsTests
    {
        private ProductCreateArguments _args;
        public ProductCreateArgumentsTests()
        {
            _args = GenFu.GenFu.New<ProductCreateArguments>();
        }

        [Fact]
        public void ProductCreateArguments_NameIsRequired()
        {
            // Arrange 
            _args.Name = null;

            // Act
            Func<IEnumerable<KeyValuePair<string, string>>> func = () => StripeClient.GetKeyValuePairs(_args);

            // Assert
            func.Enumerating().ShouldThrow<ValidationException>();
        }

        [Fact]
        public void ProductCreateArguments_PackageDimension()
        {
            // Arrange 
            _args.PackageDimensions = GenFu.GenFu.New<PackageDimensions>();

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().Contain(x => x.Key == "package_dimensions[height]")
                .And.Contain(x => x.Key == "package_dimensions[length]")
                .And.Contain(x => x.Key == "package_dimensions[weight]")
                .And.Contain(x => x.Key == "package_dimensions[width]");
        }

        [Fact]
        public void ProductCreateArguments_GetAllKeys()
        {
            // Arrange 
            _args.Metadata = Data.Metadata;
            _args.Attributes = Data.Attributes;
            _args.Shippable = true;
            _args.Active = true;

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(10)
                .And.Contain(x => x.Key == "active")
                .And.Contain(x => x.Key == "attributes[color]")
                .And.Contain(x => x.Key == "attributes[size]")
                .And.Contain(x => x.Key == "caption")
                .And.Contain(x => x.Key == "description")
                .And.Contain(x => x.Key == "id")
                .And.Contain(x => x.Key == "metadata[key1]")
                .And.Contain(x => x.Key == "metadata[key2]")
                .And.Contain(x => x.Key == "name")
                .And.Contain(x => x.Key == "shippable");
        }
    }
}