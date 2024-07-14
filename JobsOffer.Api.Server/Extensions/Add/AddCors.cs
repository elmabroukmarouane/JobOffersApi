using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobsOffer.Api.Server.Extensions.Add;
public static class AddCors
{
    public static void AddCORS(this IServiceCollection self, IConfiguration configuration)
    {
        self.AddCors(option =>
        {
            option.AddPolicy(configuration.GetSection("CorsName").Value ?? "",
                builder => builder.AllowAnyOrigin()
                                  .AllowAnyMethod()
                                  .AllowAnyHeader()
                                  .Build()
                            );
        });
    }
}
