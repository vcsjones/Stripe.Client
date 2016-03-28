using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Stripe.Client.Sdk.Clients;
using Stripe.Client.Sdk.Clients.Core;
using Stripe.Client.Sdk.Models;
using Stripe.Client.Sdk.Models.Arguments;
using Xunit;

namespace Stripe.Client.Sdk.Tests.Clients.Core
{
    public class TokenClientTests
    {
        private readonly CancellationToken _cancellationToken = CancellationToken.None;
        private ITokenClient _client;
        private IStripeClient _stripe;

        public TokenClientTests()
        {
            _stripe = Substitute.For<IStripeClient>();
            _client = new TokenClient(_stripe);
        }

        [Fact]
        public async Task GetTokenTest()
        {
            // Arrange
            var id = "token-id";
            _stripe.Get(Arg.Is<StripeRequest<Token>>(a => a.UrlPath == "tokens/" + id), _cancellationToken)
                .Returns(Task.FromResult(new StripeResponse<Token>()));

            // Act
            var response = await _client.GetToken(id, _cancellationToken);

            // Assert
            response.Should().NotBeNull();
        }

        [Fact]
        public async Task CreateCardTokenTest()
        {
            // Arrange
            var args = new CardTokenCreateArguments();
            _stripe.Post(
                Arg.Is<StripeRequest<CardTokenCreateArguments, Token>>(a => a.UrlPath == "tokens" && a.Model == args),
                _cancellationToken).Returns(Task.FromResult(new StripeResponse<Token>()));

            // Act
            var response = await _client.CreateCardToken(args, _cancellationToken);

            // Assert
            response.Should().NotBeNull();
        }

        [Fact]
        public async Task CreateBankAccountTokenTest()
        {
            // Arrange
            var args = new BankAccountTokenCreateArguments();
            _stripe.Post(
                Arg.Is<StripeRequest<BankAccountTokenCreateArguments, Token>>(
                    a => a.UrlPath == "tokens" && a.Model == args), _cancellationToken)
                .Returns(Task.FromResult(new StripeResponse<Token>()));

            // Act
            var response = await _client.CreateBankAccountToken(args, _cancellationToken);

            // Assert
            response.Should().NotBeNull();
        }

        [Fact]
        public async Task CreatePiiTokenTest()
        {
            // Arrange
            var args = new PiiTokenCreateArguments();
            _stripe.Post(
                Arg.Is<StripeRequest<PiiTokenCreateArguments, Token>>(
                    a => a.UrlPath == "tokens" && a.Model == args), _cancellationToken)
                .Returns(Task.FromResult(new StripeResponse<Token>()));

            // Act
            var response = await _client.CreatePiiToken(args, _cancellationToken);

            // Assert
            response.Should().NotBeNull();
        }
    }
}