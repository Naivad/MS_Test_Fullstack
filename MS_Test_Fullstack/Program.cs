using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MS_Test_Fullstack.Domain.Interfaces;
using MS_Test_Fullstack.Domain.IReposotories;
using MS_Test_Fullstack.Infrastructure;
using MS_Test_Fullstack.Infrastructure.Repositories;
using MS_Test_Fullstack.Services;
using System.Data;
using System.Data.SqlClient;


var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((context, services) =>
    {
       

        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddScoped<IDataAccessRepository, GenericRepository>();
        services.AddScoped<IFlightsRepository, FlightsRepository>();
        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

        services.AddScoped<IFlightsServices, FlightsServices>();

    })
    .Build();

host.Run();