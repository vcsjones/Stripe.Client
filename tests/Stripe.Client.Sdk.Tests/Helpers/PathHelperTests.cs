using Xunit;
using Stripe.Client.Sdk.Helpers;

namespace Stripe.Client.Sdk.Tests.Helpers
{
    public class PathHelperTests
    {
        [Fact]
        public void GetPathTest_ShouldConcatenate()
        {
            var path = PathHelper.GetPath("a", "b", "c");
            Assert.Equal("a/b/c", path);
        }

        [Fact]
        public void GetPathTest_ShouldTrimLeadingSlash()
        {
            var path = PathHelper.GetPath("/a", "b", "c");
            Assert.Equal("a/b/c", path);
        }

        [Fact]
        public void GetPathTest_ShouldTrimTrailingSlash()
        {
            var path = PathHelper.GetPath("a/", "b", "c/");
            Assert.Equal("a/b/c", path);
        }

        [Fact]
        public void GetPathTest_ShouldTrimLeadingAndTrailingWhitespace()
        {
            var path = PathHelper.GetPath("a ", "b", " c");
            Assert.Equal("a/b/c", path);
        }
    }



}