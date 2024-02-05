// See https://aka.ms/new-console-template for more information
using CloverParser.Helpers;
using CloverParser.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder().ConfigureServices(
    services => {
        services.AddSingleton<IFileParser, FileParser>();
        services.AddSingleton<ISpecProcessor, SpecProcessor>();
        services.AddSingleton<IDataProcessor, DataProcessor>();
        services.AddSingleton<ITypeHelper, TypeHelper>();
    }).Build();

var app = host.Services.GetRequiredService<IFileParser>();
app.Parse();