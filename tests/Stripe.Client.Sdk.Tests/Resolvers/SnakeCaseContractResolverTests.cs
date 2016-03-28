﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Newtonsoft.Json;
using Stripe.Client.Sdk.Models;
using Stripe.Client.Sdk.Resolvers;

namespace Stripe.Client.Sdk.Tests.Resolvers
{
    public class SnakeCaseContractResolverTests
    {
        [Fact]
        public void ResolvePropertyName_ShouldSerialize()
        {
            // Arrange
            var model = GenFu.GenFu.New<Sample>();

            // Act
            var json = JsonConvert.SerializeObject(model, new JsonSerializerSettings
            {
                ContractResolver = new SnakeCaseContractResolver()
            });

            // Assert
            json.Should().Contain("some_prop")
                .And.Contain("prop_with_number1")
                .And.Contain("justprop");
        }

        [Fact]
        public void ResolvePropertyName_ShouldDeserialize()
        {
            // Arrange
            var json = "{\"a\":\"It's always...\", \"some_prop\":\"Marsha!\",\"prop_with_number1\":\"Marsha!!\",\"justprop\":\"Marsha!!!\"}";

            // Act
            var model = JsonConvert.DeserializeObject<Sample>(json, new JsonSerializerSettings
            {
                ContractResolver = new SnakeCaseContractResolver()
            });

            // Assert
            model.A.Should().Be("It's always...");
            model.SomeProp.Should().Be("Marsha!");
            model.PropWithNumber1.Should().Be("Marsha!!");
            model.Justprop.Should().Be("Marsha!!!");
        }

        private class Sample
        {
            public string A { get; set; }
            public string SomeProp { get; set; }
            public string PropWithNumber1 { get; set; }
            public string Justprop { get; set; }
        }
    }
}