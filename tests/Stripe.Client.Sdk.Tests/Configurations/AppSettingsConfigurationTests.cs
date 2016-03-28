using System;
using FluentAssertions;
using Xunit;
using Stripe.Client.Sdk.Configuration;

namespace Stripe.Client.Sdk.Tests.Configurations
{
    public class AppSettingsConfigurationTests
    {
        private readonly IStripeConfiguration _configuration = new AppSettingsConfiguration();

        [Fact]
        public void AppSettingsConfiguration_AccountId()
        {
            _configuration.AccountId.Should().Be("the-account-id");
        }

        [Fact]
        public void AppSettingsConfiguration_PublishableKey()
        {
            _configuration.PublishableKey.Should().Be("the-publishable-key");
        }

        [Fact]
        public void AppSettingsConfiguration_SecretKey()
        {
            _configuration.SecretKey.Should().Be("the-secret-key");
        }
    }
}