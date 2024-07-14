using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Npgsql;
using JobsOffer.Api.Infrastructure.DatabaseContext.DbContextJobsOffer;

namespace JobsOffer.Api.Server.Extensions.Add;
public static class AddConnexion
{
    public static void AddConnection(this IServiceCollection self, IConfiguration configuration,
        IHostEnvironment env)
    {
        self.AddDbContext<DbContextJobsOffer>(options =>
        {
            switch (configuration.GetSection("ConnectionType").Value)
            {
                case "SQLSERVER":
                    UseSqlServer(SqlServerConnectionStringBuilderFunction("SqlServerConnection", "SqlServerDbPassword", configuration), options, configuration, env);
                    break;
                case "POSTGRESQL":
                    UsePostgreSql(PostgreSqlConnectionStringBuilderFunction("PostgreSqlConnection", "PostgreSqlDbPassword", configuration), options, configuration, env);
                    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
                    break;
                default:
                    UseSqlServer(SqlServerConnectionStringBuilderFunction("SqlServerConnection", "SqlServerDbPassword", configuration), options, configuration, env);
                    break;
            }
        });
        static string SqlServerConnectionStringBuilderFunction(string connectionStringName, string ConnectionConfigPart,
            IConfiguration configuration)
        {
            var builer = new SqlConnectionStringBuilder(
                    configuration.GetConnectionString(connectionStringName)
                );
            builer.Password = configuration.GetConnectionString(ConnectionConfigPart);
            return builer.ConnectionString;
        }

        static void UseSqlServer(string connectionString, DbContextOptionsBuilder options,
            IConfiguration configuration, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                options
                    .UseSqlServer(
                            connectionString,
                            options => options.EnableRetryOnFailure()
                        ).EnableSensitiveDataLogging().EnableDetailedErrors();
            }
            options
                .UseSqlServer(
                        connectionString,
                        options => options.EnableRetryOnFailure()
                    );
        }

        static string PostgreSqlConnectionStringBuilderFunction(string connectionStringName, string ConnectionConfigPart,
            IConfiguration configuration)
        {
            var builer = new NpgsqlConnectionStringBuilder(
                    configuration.GetConnectionString(connectionStringName)
                );
            builer.Password = configuration.GetConnectionString(ConnectionConfigPart);
            return builer.ConnectionString;
        }
        static void UsePostgreSql(string connectionString, DbContextOptionsBuilder options,
            IConfiguration configuration, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                options.UseNpgsql(
                            connectionString,
                            options => options.EnableRetryOnFailure()
                        ).EnableSensitiveDataLogging().EnableDetailedErrors();
            }
            options.UseNpgsql(
                connectionString,
                options => options.EnableRetryOnFailure()
                );
        }
    }
}
