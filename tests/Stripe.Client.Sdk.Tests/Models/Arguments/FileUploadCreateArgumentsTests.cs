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
    public class FileUploadCreateArgumentsTests
    {
        private FileUploadCreateArguments _args;
        public FileUploadCreateArgumentsTests()
        {
            _args = GenFu.GenFu.New<FileUploadCreateArguments>();
        }

        [Fact(Skip = "Ignore")]
        public void FileUploadCreateArguments_TheFieldIsRequired()
        {
            // Arrange 
            // _args.TheField = null;

            // Act
            Func<IEnumerable<KeyValuePair<string, string>>> func = () => StripeClient.GetKeyValuePairs(_args);

            // Assert
            func.Enumerating().ShouldThrow<ValidationException>();
        }

        [Fact(Skip = "Ignore")]
        public void FileUploadCreateArguments_DoesNotHaveRequiredFields()
        {
            // Arrange 
            _args = new FileUploadCreateArguments();

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(0);
        }

        [Fact(Skip = "Ignore")]
        public void FileUploadCreateArguments_GetAllKeys()
        {
            // Arrange 

            // Act
            var keyValuePairs = StripeClient.GetKeyValuePairs(_args).ToList();

            // Assert
            keyValuePairs.Should().HaveCount(0)
                .And.NotContain(x => x.Key == "id")
                .And.Contain(x => x.Key == "starting_after")
                .And.Contain(x => x.Key == "limit");
        }
    }
}