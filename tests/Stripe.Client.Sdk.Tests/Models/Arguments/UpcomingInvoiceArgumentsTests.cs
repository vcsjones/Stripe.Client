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
    public class UpcomingInvoiceArgumentsTests
    {
        private UpcomingInvoiceArguments _args;
        public UpcomingInvoiceArgumentsTests()
        {
            _args = GenFu.GenFu.New<UpcomingInvoiceArguments>();
        }

        [Fact]
        public void UpcomingInvoiceListFilter_DoesNotHaveRequiredFields()
        {
            // Arrange 
            _args = new UpcomingInvoiceArguments();

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(0);
        }

        [Fact]
        public void UpcomingInvoiceListFilter_GetAllKeys()
        {
            // Arrange
            _args.SubscriptionProrate = true;
            _args.SubscriptionProrationDate = DateTime.UtcNow;
            _args.SubscriptionTrialEnd = DateTime.UtcNow;

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(7)
                .And.Contain(x => x.Key == "customer")
                .And.Contain(x => x.Key == "subscription")
                .And.Contain(x => x.Key == "subscription_plan")
                .And.Contain(x => x.Key == "subscription_prorate")
                .And.Contain(x => x.Key == "subscription_proration_date")
                .And.Contain(x => x.Key == "subscription_quantity")
                .And.Contain(x => x.Key == "subscription_trial_end");
        }
    }
}