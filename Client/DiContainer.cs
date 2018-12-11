using System;
using Client.Login;
using Microsoft.Extensions.DependencyInjection;

namespace Client
{
    class DiContainer
    {
        public IServiceProvider ServiceProvider { get; }

        public DiContainer()
        {
            // Configure services
            ServiceProvider = RegisterServices();
        }

        private  IServiceProvider RegisterServices()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureHttpClients(serviceCollection);
            return serviceCollection.BuildServiceProvider();
        }

        private  void ConfigureHttpClients(IServiceCollection services)
        {
            services.AddHttpClient<LoginService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44335/api/user/");
                client.DefaultRequestHeaders.Add("Accept", "text/html");
                client.DefaultRequestHeaders.Add("User-Agent", "vojo");
            });
          
        }
    }
}


