using NSubstitute;
using Stems.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace StemsTests.Services
{
    public class SteamsFactoryTests
    {
        private readonly IGitHubClient _gitHubClient;
        public SteamsFactoryTests()
        {
            _gitHubClient = Substitute.For<IGitHubClient>();
        }

        private SteamsFactory CreateSut()
        {
            return new SteamsFactory(_gitHubClient);
        }

        [Fact]
        public async Task CreateSteams_NullStem_ReturnsEmptyItems()
        {
            var sut = CreateSut();

            var actual = await sut.CreateSteams(null);

            //Assert
            Assert.Empty(actual);

            _gitHubClient.DidNotReceiveWithAnyArgs().GetDataAsync();
        }

        [Fact]
        public async Task CreateSteams_WithStem_ReturnsPrefixedItems()
        {
            var stem = "abc";
            var stems = new List<string> { "abcde", "Abcdef", "123" };
            var expected = new List<string> { "abcde", "Abcdef" };

            _gitHubClient.GetDataAsync().Returns(stems);

            var sut = CreateSut();

            var actual = await sut.CreateSteams(stem);

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
