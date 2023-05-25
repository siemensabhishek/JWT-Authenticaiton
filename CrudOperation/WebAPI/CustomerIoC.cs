using Microsoft.Extensions.DependencyInjection;

namespace WebAPI
{
    public class CustomerIoC
    {
        private IServiceCollection _serviceCollection;
        private IConfiguration _config;

        public CustomerIoC(IServiceCollection servicesCollection,IConfiguration config) {
            _serviceCollection = servicesCollection;
                _config = config;
            _serviceCollection.AddSingleton<CustomerEntities.CustomerEntities>();
        }
    }
}
