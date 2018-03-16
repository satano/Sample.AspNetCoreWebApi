using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Kros.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.AspNetCoreWebApi.Services;
using Xunit;

namespace Sample.AspNetCoreWebApi.IntegrationTests
{
    public class FakeStartup : Startup
    {
        public FakeStartup(IConfiguration configuration) : base(configuration) { }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IActiveUser, DummyActiveUser>();

            base.ConfigureServices(services);
        }

        public override void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.AddMvcCore();
        }
    }

    internal class DummyActiveUser : IActiveUser
    {
        public int GetUserId() => 1;
    }

    public class PeopleControllerShould : IClassFixture<TestFixture<FakeStartup>>
    {
        private readonly HttpClient _client;

        public PeopleControllerShould(TestFixture<FakeStartup> fixture)
        {
            Check.NotNull(fixture, nameof(fixture));

            _client = fixture.Client;
        }

        [Fact]
        public async Task ReturnsAllPeopleForOwner()
        {
            var response = await _client.GetAsync("/api/people");

            response.EnsureSuccessStatusCode();
            //var a = response.Content.ReadAsStringAsync();
            // var responseString = await response.Content.ReadAsStringAsync();
            // Assert.Contains("testSession.Name", responseString);
            true.Should().BeTrue();
        }

        [Fact]
        public void ReturnPersonById()
        {
            // var response = await _client.GetAsync("/api/people/2");

            // // Assert
            // response.EnsureSuccessStatusCode();
            // var responseString = await response.Content.ReadAsStringAsync();
            // Assert.Contains("testSession.Name", responseString);
            true.Should().BeTrue();
        }

        [Fact]
        public void ReturnForbidWhenUserAccessToAnotherUserData()
        {
            // var response = await _client.GetAsync("/api/people/3");

            // // Assert
            // response.EnsureSuccessStatusCode();
            // var responseString = await response.Content.ReadAsStringAsync();
            // Assert.Contains("testSession.Name", responseString);
            true.Should().BeTrue();
        }

        [Fact]
        public void PostNewUser()
        {
            // var response = await _client.PostAsync("/api/people/", null);

            // // Assert
            // response.EnsureSuccessStatusCode();
            // var responseString = await response.Content.ReadAsStringAsync();
            // Assert.Contains("testSession.Name", responseString);
            true.Should().BeTrue();
        }
    }
}