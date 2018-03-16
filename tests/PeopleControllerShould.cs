using System.Net.Http;
using System.Threading.Tasks;
using Kros.Utils;
using Xunit;

namespace Sample.AspNetCoreWebApi.IntegrationTests
{
    public class PeopleControllerShould : IClassFixture<TestFixture<Startup>>
    {
        private readonly HttpClient _client;

        public PeopleControllerShould(TestFixture<Startup> fixture)
        {
            Check.NotNull(fixture, nameof(fixture));

            _client = fixture.Client;
        }

        [Fact]
        public async Task ReturnsAllPeopleForOwner()
        {
            var response = await _client.GetAsync("/api/people");

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("testSession.Name", responseString);
        }

        [Fact]
        public async Task ReturnPersonById()
        {
            var response = await _client.GetAsync("/api/people/2");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("testSession.Name", responseString);
        }

        [Fact]
        public async Task ReturnForbidWhenUserAccessToAnotherUserData()
        {
            var response = await _client.GetAsync("/api/people/3");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("testSession.Name", responseString);
        }

        [Fact]
        public async Task PostNewUser()
        {
            var response = await _client.PostAsync("/api/people/", null);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("testSession.Name", responseString);
        }
    }
}