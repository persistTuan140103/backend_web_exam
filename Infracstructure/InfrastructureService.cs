using Infracstructure.Identities;
using Infracstructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracstructure
{
    public static class InfrastructureService
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContextService(configuration)
                    .AddIdentityService(configuration)
                    .AddJwtService(configuration)
                    .AddGoogleService(configuration);

            return services;
        }
    }
}
