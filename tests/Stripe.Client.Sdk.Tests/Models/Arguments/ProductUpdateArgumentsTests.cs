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
    public class ProductUpdateArgumentsTests
    {
        private ProductUpdateArguments _args;
        public ProductUpdateArgumentsTests()
        {
            _args = GenFu.GenFu.New<ProductUpdateArguments>();
        }

        [Fact]
        public void ProductUpdateArguments_DoesNotHaveRequiredFields()
        {
            // Arrange 
            _args = new ProductUpdateArguments();

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(0);
        }

        [Fact]
        public void ProductUpdateArguments_PackageDimension()
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
        public void ProductUpdateArguments_GetAllKeys()
        {
            // Arrange 
            _args.Metadata = Data.Metadata;
            _args.Attributes = Data.Attributes;
            _args.Shippable = true;
            _args.Active = true;

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(9)
                .And.Contain(x => x.Key == "active")
                .And.Contain(x => x.Key == "attributes[color]")
                .And.Contain(x => x.Key == "attributes[size]")
                .And.Contain(x => x.Key == "caption")
                .And.Contain(x => x.Key == "description")
                .And.Contain(x => x.Key == "metadata[key1]")
                .And.Contain(x => x.Key == "metadata[key2]")
                .And.Contain(x => x.Key == "name")
                .And.Contain(x => x.Key == "shippable");
        }
    }
}