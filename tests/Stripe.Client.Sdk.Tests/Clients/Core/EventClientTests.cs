using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Stripe.Client.Sdk.Clients;
using Stripe.Client.Sdk.Clients.Core;
using Stripe.Client.Sdk.Models;
using Stripe.Client.Sdk.Models.Filters;
using Xunit;

namespace Stripe.Client.Sdk.Tests.Clients.Core
{
    public class EventClientTests
    {
        private readonly CancellationToken _cancellationToken = CancellationToken.None;
        private IEventClient _client;
        private IStripeClient _stripe;

        public EventClientTests()
        {
            _stripe = Substitute.For<IStripeClient>();
            _client = new EventClient(_stripe);
        }

        [Fact]
        public async Task GetEventTest()
        {
            // Arrange
            var id = "event-id";
            _stripe.Get(Arg.Is<StripeRequest<Event>>(a => a.UrlPath == "events/" + id), _cancellationToken)
                .Returns(Task.FromResult(new StripeResponse<Event>()));

            // Act
            var response = await _client.GetEvent(id, _cancellationToken);

            // Assert
            response.Should().NotBeNull();
        }

        [Fact]
        public async Task GetEventsTest()
        {
            // Arrange
            var filter = new EventListFilter();
            _stripe.Get(
                Arg.Is<StripeRequest<EventListFilter, Pagination<Event>>>(
                    a => a.UrlPath == "events" && a.Model == filter), _cancellationToken)
                .Returns(Task.FromResult(new StripeResponse<Pagination<Event>>()));

            // Act
            var response = await _client.GetEvents(filter, _cancellationToken);

            // Assert
            response.Should().NotBeNull();
        }
    }
}