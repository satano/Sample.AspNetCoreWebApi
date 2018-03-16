using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Kros.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Sample.AspNetCoreWebApi.IntegrationTests
{
    /// <summary>
    /// A test fixture which hosts the target project (project we wish to test) in an in-memory server.
    /// </summary>
    /// <typeparam name="TStartup">Target project's startup type</typeparam>
    public class TestFixture<TStartup> : IDisposable
    {
        private readonly TestServer _server;

        public TestFixture() : this(Path.Combine("src")) { }

        protected TestFixture(string relativeTargetProjectParentDir)
        {
            var startupAssembly = typeof(TStartup).GetTypeInfo().Assembly;
          //  var contentRoot = GetProjectPath(relativeTargetProjectParentDir, startupAssembly);

            var builder = new WebHostBuilder()
                .ConfigureServices(InitializeServices)
                .UseEnvironment("Development")
                .UseStartup(typeof(TStartup));

            _server = new TestServer(builder);

            Client = _server.CreateClient();
            Client.BaseAddress = new Uri("http://localhost");
        }

        public HttpClient Client { get; }

        public void Dispose()
        {
            Client?.Dispose();
            _server?.Dispose();
        }

        protected virtual void InitializeServices(IServiceCollection services)
        {
            var startupAssembly = typeof(TStartup).GetTypeInfo().Assembly;

            // Inject a custom application part manager.
            // Overrides AddMvcCore() because it uses TryAdd().
            var manager = new ApplicationPartManager();
            manager.ApplicationParts.Add(new AssemblyPart(startupAssembly));
            manager.FeatureProviders.Add(new ControllerFeatureProvider());
            manager.FeatureProviders.Add(new ViewComponentFeatureProvider());

            services.AddSingleton(manager);
        }
    }

    public class HttpTestClient
    {
        private string _token = null;

        public HttpTestClient(HttpClient httpClient)
        {
            HttpClient = Check.NotNull(httpClient, nameof(httpClient));
        }

        public HttpClient HttpClient { get; }

        public void Login(string tokenUrl, string email, string password) =>
            Login("\token", email, password);

        public void Login(string email, string password)
        {
            var login = new { Email = email, Password = password };
            //JsonConvert.SerializeObject(<your object>);
            //client.PostAsync(uri, new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            //HttpClient.PostAsync()
        }

        public async Task<HttpResponseMessage> GetAsync(string requestUri) =>
            await HttpClient.GetAsync(requestUri);
    }
}