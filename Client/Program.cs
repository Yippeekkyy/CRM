using System;
using System.Configuration;
using System.IO;
using Client.Options;
using Client.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Client;

public class Program
{
    public static IConfiguration Configuration { get; private set; }
    
    
    [STAThread]
    public static void Main()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // Укажите путь к файлу appsettings.json
            .AddJsonFile("appsettingsClient.json").Build();

 
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddSingleton<App>();
                services.AddSingleton<MainWindow>();
                services.AddSingleton<MainViewModel>();

                services.Configure<BackendOptions>(Configuration.GetSection("Backend"));

                
                services.AddHttpClient("HttpClient");

            })
            .Build();

        
        
      
        var app = host.Services.GetService<App>();
   
        app?.Run();
    }
}